# car-controlling

:information_source: Overview

This repository is a Unity (C# framework) application composed of some car games. Its inspiration came from a graphic computing project whose theme was "car controlling 
via keyboard".  

The performance of the users in the games is expressed through a score, which can be saved in a server via HTTP request to a Django API.
In order to have their scores saved, the users must create an account in a Django web application, whose credentials (username and password) must be provided via a form
when uploading the scores at the end of a game.

Such scores are used to build a ranking, which can be accessed via web application (in the browser or by calling the API) and via Android application. The Django 
application and the Android application can be found in the following repositories:

Django application: https://github.com/HumbertoBPF/car-controlling-web 
yo
Android application: https://github.com/HumbertoBPF/car-controlling-mobile

:arrow_down: Installation instructions

This section aims to guide you in the process of the project installation on your machine such that you are able to run the game. In order to do that, the following steps are required:

- Download Unity Hub from the Unity project web page.
- Launch Unity Hub app. It will ask you to create a Unity Account/ID and to sign into it. Please, do it.
- Once you are signed in, download the Unity Editor from the Unity Hub. It may take a considerable time (around 1h).
- Open the project on the Unity Editor when its download ends. Probably a message will be shown asking you to open with another editor version. That's because the Unity project has frequent updates. Select the suggested editor version.
- On the Unity Editor interface, select the MenuScene on the project folder section.
- On the scene section, select a 2D view (superior part of this mini window)
- On the game section, select "16:9 Aspect" as aspect ratio.
- Launch the game by clicking on the play button on the top of the window.
