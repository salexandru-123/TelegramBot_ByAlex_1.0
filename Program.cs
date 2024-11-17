using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Text.RegularExpressions;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Extensions;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using TL;


using var client = new WTelegram.Client(ConfigClass.Config);


var logInfo = await client.LoginUserIfNeeded();
// Store bot screaming status
var screaming = false;
var chatId = "";
List<string> members = new();

// Pre-assign menu text
const string firstMenu = "<b>Menu 1</b>\n\nA beautiful menu with a shiny inline button.";
const string secondMenu = "<b>Menu 2</b>\n\nA better menu with even more shiny inline buttons.";

// Pre-assign button text
const string nextButton = "Next";
const string backButton = "Back";
const string tutorialButton = "Tutorial";


// Build keyboards
InlineKeyboardMarkup firstMenuMarkup = new(InlineKeyboardButton.WithCallbackData(nextButton));
InlineKeyboardMarkup secondMenuMarkup = new(
    new[] {
        new[] { InlineKeyboardButton.WithCallbackData(backButton) },
        new[] { InlineKeyboardButton.WithUrl(tutorialButton, "https://core.telegram.org/bots/tutorial") }
    }
);

var bot = new TelegramBotClient("7621712307:AAFaYoM2JOhi9YjVYiunmdMLCnjQuzFxTIc");

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool, so we use cancellation token
bot.StartReceiving(
    updateHandler: HandleUpdate,
    errorHandler: HandleError,
    cancellationToken: cts.Token
);

// Tell the user the bot is online
Console.WriteLine("Start listening for updates. Press enter to stop");
Console.ReadLine();

// Send cancellation request to stop the bot
cts.Cancel();

// Each time a user interacts with the bot, this method is called
async Task HandleUpdate(ITelegramBotClient _, Telegram.Bot.Types.Update update, CancellationToken cancellationToken)
{
    
    switch (update.Type)
    {
        // A message was received
        case UpdateType.Message:
            await HandleMessage(update.Message!);
            break;

        // A button was pressed
        case UpdateType.CallbackQuery:
            await HandleButton(update.CallbackQuery!);
            break;
    }
}

async Task HandleError(ITelegramBotClient _, Exception exception, CancellationToken cancellationToken)
{
    await Console.Error.WriteLineAsync(exception.Message +Environment.NewLine+ exception.StackTrace);
}

async Task HandleMessage(Telegram.Bot.Types.Message msg)
{
    var user = msg.From;
    var text = msg.Text ?? string.Empty;
    chatId = msg.Chat.Id.ToString();
    Debug.WriteLineIf(chatId != "",$"Chat Id: {chatId}");
    ChatFullInfo chatInfo = await bot.GetChat(chatId);
    Debug.WriteLineIf(chatInfo != null,chatInfo.ToString());
    Debug.WriteLineIf(members.Count > 0,$"Members found: {members.Count}");
    if (user is null)
        return;

    // Print to console
    Console.WriteLine($"{user.FirstName} wrote {text}");

    // When we get a command, we react accordingly
    if (text.StartsWith("/"))
    {
        await HandleCommand(user.Id, text);
    }
    else if (screaming && text.Length > 0)
    {
        // To preserve the markdown, we attach entities (bold, italic..)
        await bot.SendTextMessageAsync(user.Id, text.ToUpper(), entities: msg.Entities);
    }
    else
    {   // This is equivalent to forwarding, without the sender's name
        await bot.CopyMessageAsync(user.Id, user.Id, msg.MessageId);
    }
}


async Task HandleCommand(long userId, string command)
{
    string message = "";
    switch (command)
    {
        case "/help":
            message = $"Commands list:{Environment.NewLine}"+
            $"/help{Environment.NewLine}/all";
            await bot.SendMessage(chatId, message);
            break;

        case "/weather":
            
            break;

        case "/all":
            //make it so it makes a message with everyone name from chat with '@' before the username
            var chats = await client.Messages_GetAllChats();
            var channel = (Channel)chats.chats[2267067820]; // the channel we want
            for (int offset = 0; ;)
            {
                var participants = await client.Channels_GetParticipants(channel, null, offset);
                foreach (var (id, user) in participants.users)
                    message += $"{user} ";
                offset += participants.participants.Length;
                if (offset >= participants.count || participants.participants.Length == 0) break;
            }
            
            await bot.SendMessage(chatId, message);
            
            break;
        case "/scream":
            screaming = true;
            await bot.SendMessage(chatId, "AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
            break;

        case "/whisper":
            screaming = false;
            await bot.SendAnimation(chatId,  "https://giphy.com/gifs/cartoonnetwork-dexters-laboratory-lab-dexter-G3EGcAf1Lj1AXTLN9Q");
            break;

        case "/menu":
            await SendMenu(userId);
            break;

        default:
            break;
    }

    await Task.CompletedTask;
}

async Task SendMenu(long userId)
{
    await bot.SendTextMessageAsync(
        userId,
        firstMenu,
        parseMode:ParseMode.Html,
        replyMarkup: firstMenuMarkup
    );
}

async Task HandleButton(CallbackQuery query)
{
    string text = string.Empty;
    InlineKeyboardMarkup markup = new(Array.Empty<InlineKeyboardButton>());

    if (query.Data == nextButton)
    {
        text = secondMenu;
        markup = secondMenuMarkup;
    }
    else if (query.Data == backButton)
    {
        text = firstMenu;
        markup = firstMenuMarkup;
    }

    // Close the query to end the client-side loading animation
    await bot.AnswerCallbackQueryAsync(query.Id);

    // Replace menu text and keyboard
    await bot.EditMessageTextAsync(
        query.Message!.Chat.Id,
        query.Message.MessageId,
        text,
        ParseMode.Html,
        replyMarkup: markup
    );
}
