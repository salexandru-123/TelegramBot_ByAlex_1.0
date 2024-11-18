<h1>Docker Installation</h1>
<ul>
    <li>Download the Dockerfile</li>
    <li>Open terminal where the Dockerfile is</li>
    <li>Use this command to make the image '  docker build -t telegrambot_byalex-image -f Dockerfile . '</li>
    <li>Create a container ' docker create --name core-telegrambot_byalex telegrambot_byalex-image '</li>
    <li>Find the image id with ' docker ps -a '</li>
    <li>Start the image with ' docker run -it --rm [docker_image_id_here] '</li>
</ul>