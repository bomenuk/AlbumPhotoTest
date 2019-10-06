# AlbumPhotoTest

## Overview

This solution is built with C# .Net Core in VS 2019 to retreive Album & Photo data from public api:

http://jsonplaceholder.typicode.com/photos 
http://jsonplaceholder.typicode.com/albums 

respectively.

## Build & Run

Open the solution in VS2019, right-click on solution choose "Restore Nuget packages", rebuild then Ctrl+F5 to launch it inside browser.

By default, it should open a browser tab for url like: https://localhost:44395/api/album which will load all albums & their photos data from publi api mentioned above & display as lines of strings that contain information retrieved.

You can also go to for example https://localhost:44395/api/album/user/1 to load data for userID: 1 only.

The solution also use a built in memory cache client to store data loaded from public api above for quick access because I find it was a bit too slow when every time refresh the page during debugging.