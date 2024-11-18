<h1>Docker Installation</h1>
<ul>
    <li>Download this repository</li>
    <li>Open terminal where the Dockerfile is</li>
    <li>Use this command to make the image <code>docker build -t telegrambot_byalex-image -f Dockerfile .</code></li>
    <li>Create a container <code>docker create --name core-telegrambot_byalex telegrambot_byalex-image</code></li>
    <li>Find the image id with <code>docker images</code></li>
    <li>Start the image with <code>docker run -it --rm [docker_image_id_here]</code></li>
</ul>
