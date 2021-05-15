"use strict";


//const video = document.getElementById('video')
const peerVideo = document.querySelector('#peerVideo');
const myVideo = document.querySelector('#myVideo');
const myId = document.querySelector('#myId');
const peerId = document.querySelector('#peerId');
const peerInput = document.getElementById("peerInput");
const callBtn = document.getElementById("callButton");
const hangupBtn = document.getElementById("hangupButton");

//api
const uri = "https://localhost:5001/api/Sessions";



Promise.all([
    faceapi.nets.tinyFaceDetector.loadFromUri('/face-api/models'),
    faceapi.nets.faceLandmark68Net.loadFromUri('/face-api/models'),
    faceapi.nets.faceRecognitionNet.loadFromUri('/face-api/models'),
    faceapi.nets.faceExpressionNet.loadFromUri('/face-api/models')

])


let randomId = Math.random().toString(36).substring(3);
let expressions = [];
let intervalCheck = 0;

const peer = new Peer(key, {
    host: 'localhost',
    port: 9000,
    //host: 'peerjsserverdemo20200405055319.azurewebsites.net',
    //port: 443,
    path: '/myapp',
    secure: false
});

peer.on('open', function (id) {

    console.log('My peer ID is: ' + id);

    myId.innerText = id;

});

peer.on('call', function (mediaConnection) {

    navigator.mediaDevices
        .getUserMedia({
            audio: true,
            video: true
        })
        .then(function (mediaStream) {

            myVideo.srcObject = mediaStream;

            // Answer the call, providing our mediaStream
            mediaConnection.answer(mediaStream);

            mediaConnection.on('stream', function (stream) {
                // `stream` is the MediaStream of the remote peer.
                // Here you'd add it to an HTML video/canvas element.
                peerVideo.srcObject = stream;
            });
        });
});


callBtn.addEventListener("click", function (event) {
    var user = peerInput.value;

    navigator.mediaDevices
        .getUserMedia({
            audio: true,
            video: true
        })
        .then(function (mediaStream) {

            myVideo.srcObject = mediaStream;

            // Call a peer, providing our mediaStream
            var call = peer.call(user, mediaStream);

            call.on('stream', function (stream) {
                // `stream` is the MediaStream of the remote peer.
                // Here you'd add it to an HTML video/canvas element.
                peerVideo.srcObject = stream;
            });

        });

    event.preventDefault();
});

function addItem() {

    intervalCheck = 1;

    var expressionsStr = JSON.stringify(expressions);
 
    const item = {

        AppointmentId: 1,
        Expressions: expressionsStr
    };

    console.log(expressions)

    fetch(uri, {
        method: 'POST',
        headers: {
            'Accept': 'application/json',
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(item)
    })
        .then(response => response.json())
        .then(data => console.log(data))
        .catch(error => console.error('Unable to add item.', error));

}

/*hangupBtn.addEventListener("submit", function (event) {
    addItem();

});*/


peerVideo.addEventListener('play', () => {

    //const canvas = faceapi.createCanvasFromMedia(peerVideo)
    //document.body.append(canvas)
    //const displaySize = { width: peerVideo.width, height: peerVideo.height }
    //faceapi.matchDimensions(canvas, displaySize)
    var detectLoop = setInterval(async () => {

        if (intervalCheck == 1) {
            clearInterval(detectLoop);
        }

        const detections = await faceapi.detectAllFaces(peerVideo,
            new faceapi.TinyFaceDetectorOptions()).withFaceLandmarks().withFaceExpressions()

        // console.log(detections[0].expressions)
        detections[0].expressions.timeStamp = new Date().toLocaleTimeString()
        expressions.push(detections[0].expressions)
        console.log(Date.now());
        console.log("========================================");
        console.log(detections[0].expressions);
        console.log("========================================");

        //const resizeDetections = faceapi.resizeResults(detections, displaySize)
        //canvas.getContext('2d').clearRect(0, 0, canvas.width, canvas.height)
        //faceapi.draw.drawDetections(canvas, resizeDetections)
        //faceapi.draw.drawFaceLandmarks(canvas, resizeDetections)
        //faceapi.draw.drawFaceExpressions(canvas, resizeDetections)
    }, 1000)
})