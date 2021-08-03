# TrueLayerChallenge
<h3>General Information </h3>
A true layer job challenge
The project was built with Dot net core 3.1 LTS and visual studio Enterprise 2019
<h3>Running the project from Visual studion on IIS EXPRESS </h3>
1. Debug the the application (Press F5) 
2. The application defaults to http://localhost:[PORT]/api/Pokemon/mewtwo 
3. You can change the name of the specie (mewtwo) to any of the species available at the pokemon api
4. to run the translation endpoint, use http://localhost:[PORT]/api/Pokemon/translation/mewtwo 
5. get the test project from <a href="https://github.com/olayinks/TrueLayerChallenge.Test">here</a>
6. The Translation endpoint has a rate limiter of 5 calls per hour, due to the fun translation endpoint's limitation for free license use 

<h3>Running the project from Docker hub </h3>
1. pull the image from <a href="https://hub.docker.com/r/olayinks/truelayerrepo/tags?page=1&ordering=last_updated"> docker hub </a>
2. Using Docker for Desktop or Mac

<h3>API Consideration Production</h3>
1. The translation API is on a rate limiter of 5 calls within an hour for the free version,
The production API would subscribe to a paid service
2. The API should be protected behind some form of authentication and authorization service the provides OAuth2 or OpenId standards
3. Currently the choice of the language to translate is random, a more definative approach should be taken as to return
an expected feedback to the client
4. More detailed Integration test and validation test would be carried out to ensure comphrensive functional test 
5. 
