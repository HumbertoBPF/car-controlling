# car-controlling

:information_source: Overview

This repository is a Unity(C# framework) application composed of some car games. Its inspiration came from a graphic computing project whose theme was "car controlling 
via keyboard".  

The performance of the users in the games is expressed through a score, which can be saved in a server via HTTP request to a Django API.
In order to have their scores saved, the users must create an account in a Django web application, whose credentials(username and password) must be provided via a form
when uploading the scores at the end of a game.

Such scores are used to build a ranking, which can be accessed via web application(in the browser or by calling the API) and via Android application. The Django 
application and the Android application can be found in the following repositories:

Django application: https://github.com/HumbertoBPF/car-controlling-web 

Android application: https://github.com/HumbertoBPF/car-controlling-mobile
