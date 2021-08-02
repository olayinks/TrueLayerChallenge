# TrueLayerChallenge
A true layer job challenge
The project was built with Dot net core 3.1 LTS and visual studio
Running the project from Visual studion on IIS EXPRESS
1. Debug the the application (Press F5) 
2. The application defaults to http://localhost:[PORT]/api/Pokemon/mewtwo 
3. You can change the name of the specie (mewtwo) to any of the species available at the pokemin api
4. to run the translation endpoint, use http://localhost:[PORT]/api/Pokemon/translation/mewtwo 

Running the project from docker hub
pull the image from <a href="https://hub.docker.com/r/olayinks/truelayerrepo1/tags?page=1&ordering=last_updated"> docker hub </a>

Production API consideration
1. The translation API is on a rate limiter of 5 calls within an hour for the free version,
The production API would subscribe to a paid service
2. The API should be protected behind some form of authentication and authorization service like OAuth2
3. Currently the choice of the language to translate is random, a more definative approach should be taken as to return
an expected feedback to the client
4. 
