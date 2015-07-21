FROM microsoft/aspnet:vs-1.0.0-beta4

ADD . /app

WORKDIR /app/approot/src/HelloWorldWeb

ENTRYPOINT ["dnx", ".", "Kestrel", "--server.urls", "http://localhost:{{DockerPublishContainerPort}}"]