In this project I created a flight simulator which generates objects of flights and send them via Rest over to my API.

The post method of the API sends the flight object to the DB and to Azure queue

A background service waits for a flight to dequeue from the cloud

This service manages multiple flights moving each flight in the process of landing, taxing, passengers drop-off,

pickups and taking off as well.

Each process has its own counter to simulate the time a plane would take to complete these tasks.

this done with multithreading, timers and locking making sure all flights are making their progress when they are ready and without colliding with

other flights.

Each process being completed emits an update for the view using SignalR, to simulate a real time flights scheduling board, I chose to develop my view with Angular.
