"use strict";


//const video = document.getElementById('video')
const peerVideo = document.querySelector('#peerVideo');
const myVideo = document.querySelector('#myVideo');
//const myId = document.querySelector('#myId');
//const peerId = document.querySelector('#peerId');
//const peerInput = document.getElementById("peerInput");
//const callBtn = document.getElementById("callButton");
const hangupBtn = document.getElementById("hangupButton");
const endButton = document.getElementById("endCall");
let neutral = document.getElementById("neutral");
let happy = document.getElementById("happy");
let sad = document.getElementById("sad");
let angry = document.getElementById("angry");
let fear = document.getElementById("fear")
let disgust = document.getElementById("disgust")
let surprise = document.getElementById("surprise")
var startTime;
var endTime;

//api
const uri = "/api/Sessions";

//luxon
var dt = luxon.DateTime;

var lx_year = dt.now().year;
var lx_month = dt.now().month;
var lx_day = dt.now().day;
var appDate = dt.local(lx_year, lx_month, lx_day).toISO();

//progressbar.js
function createBar(emotion) {
  let bar = new ProgressBar.Line(emotion, {
        strokeWidth: 15,
        easing: 'easeInOut',
        duration: 1000,
        color: '#3655d1',
        trailColor: '#eee',
        trailWidth: 15,
      svgStyle: { width: '100%', height: '100%' },
      text: {
          style: {
              // Text color.
              // Default: same as stroke color (options.color)
              color: '#999',
              position: 'absolute',
              right: '50%',
              top: '0',
              padding: 0,
              margin: 0,
              transform: null
          },
          autoStyleContainer: false
      },
      from: { color: '#FFEA82' },
      to: { color: '#ED6A5A' },
      step: (state, bar) => {
          bar.setText(Math.round(bar.value() * 100) + ' %');
      }
  });
    return bar;
}
var bar1 = createBar(neutral)
var bar2 = createBar(happy)
var bar3 = createBar(sad)
var bar4 = createBar(angry)
var bar5 = createBar(fear)
var bar6 = createBar(disgust)
var bar7 = createBar(surprise)


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
    host: 'powerful-escarpment-39791.herokuapp.com',
    port: 443,
    //host: 'peerjsserverdemo20200405055319.azurewebsites.net',
    //port: 443,
    //path: '/myapp',
    secure: true,

    config: {
        'iceServers': [
            { urls: "stun:stun.l.google.com:19302"},
            { urls: "turn:memo-turn.southcentralus.azurecontainer.io", username: "WASQ16", credential: "Wasique_1996"}
        ]
    }
});

peer.on('open', function (id) {

    console.log('My peer ID is: ' + id);

    //myId.innerText = id;

});

peer.on('call', function (mediaConnection) {

    $("#modal-waiting").modal("hide")

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


function addItem() {

    peer.destroy()
    intervalCheck = 1;

    //startTime = dt.now().toISO();
  if (startTime != null) {
      endTime = dt.now().toISO();

      var expressionsStr = JSON.stringify(expressions);

      const item = {

          AppointmentId: AppId,
          Date: appDate,
          TimeStart: startTime,
          TimeEnd: endTime,
          Expressions: expressionsStr,
          CounselorID: Cid
      };

      //console.log(expressions)

      fetch(uri, {
          method: 'POST',
          headers: {
              'Accept': 'application/json',
              'Content-Type': 'application/json'
          },
          body: JSON.stringify(item)
      })
          .then(response => response.json())
          //.then(data => console.log(data))
          .catch(error => console.error('Unable to add item.', error))
          .then(showModal());
    }
    else
        console.log("Session was empty. Skipping push.")

    
    
}

endCall.addEventListener("click", function (event) {
    addItem();
    //OnComplete();
    
});


peerVideo.addEventListener('play', () => {

    //const canvas = faceapi.createCanvasFromMedia(peerVideo)
    //document.body.append(canvas)
    //const displaySize = { width: peerVideo.width, height: peerVideo.height }
    //faceapi.matchDimensions(canvas, displaySize)
    startTime = dt.now().toISO();

    var detectLoop = setInterval(async () => {

        if (intervalCheck == 1) {
            clearInterval(detectLoop);
        }

        const detections = await faceapi.detectAllFaces(peerVideo,
            new faceapi.TinyFaceDetectorOptions()).withFaceLandmarks().withFaceExpressions()

        // console.log(detections[0].expressions)
        detections[0].expressions.timeStamp = dt.now().toISO()
        expressions.push(detections[0].expressions)
        /*console.log(Date.now());
        console.log("========================================");
        console.log(detections[0].expressions);
        console.log("========================================");*/

        var nBar1 = detections[0].expressions['neutral'];
        nBar1 = nBar1.toFixed(1);
        var hBar2 = detections[0].expressions['happy'];
        hBar2 = hBar2.toFixed(1);
        var sBar3 = detections[0].expressions['sad'];
        sBar3 = sBar3.toFixed(1);
        var aBar4 = detections[0].expressions['angry'];
        aBar4 = aBar4.toFixed(1);
        var fBar5 = detections[0].expressions['fearful'];
        fBar5 = fBar5.toFixed(1);
        var dBar6 = detections[0].expressions['disgusted'];
        dBar6 = dBar6.toFixed(1);
        var dBar7 = detections[0].expressions['surprised'];
        dBar7 = dBar7.toFixed(1);

        console.log(nBar1)

        bar1.animate(nBar1);  // Number from 0.0 to 1.0
        bar2.animate(hBar2); 
        bar3.animate(sBar3); 
        bar4.animate(aBar4); 
        bar5.animate(fBar5); 
        bar6.animate(dBar6); 
        bar7.animate(dBar7); 

        //const resizeDetections = faceapi.resizeResults(detections, displaySize)
        //canvas.getContext('2d').clearRect(0, 0, canvas.width, canvas.height)
        //faceapi.draw.drawDetections(canvas, resizeDetections)
        //faceapi.draw.drawFaceLandmarks(canvas, resizeDetections)
        //faceapi.draw.drawFaceExpressions(canvas, resizeDetections)
    }, 1000)
})

function showModal() {
    $("#modal-continue").modal("show")
}