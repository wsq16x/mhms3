//api
const uri = "/api/Sessions/" + sessionId;

//luxon
var dt = luxon.DateTime

//var ctx = document.getElementById('myChart').getContext('2d');

var ChartData;
var neutral = [];
var happy= []; 
var sad = [];
var angry = [];
var fearful = [];
var disgusted =  [];
var surprised = [];
var timestamp = [];
let counter = [0, 0, 0, 0, 0, 0, 0]

function getItem() {
    fetch(uri)
        .then(response => response.json())
        .then(data => saveData(data))
        .catch(error => console.error('Unable to get items.', error));
 }

function saveData(data){
    var expressions = JSON.parse(data.expressions);
    expressions.map(obj => {
        let keys = Object.keys(obj);
        // console.log(keys);
        let max = obj[keys[0]];
        let i;
        let maxIndex = 0;

        for (i = 1; i < keys.length; i++) {
            let value = obj[keys[i]];
            // if (value < min) 
            // min = value;
            if (value > max) {
                max = value;
                maxIndex = i;

            }
        }
        counter[maxIndex] += 1;
       /* console.log(counter);*/
    })

    mapData(expressions)
}

getItem();

function mapData(data) {
    data.map(obj => {
        let keys = Object.keys(obj);
        keys.map(key => {
            if (key == "neutral") {
                obj[key] = obj[key].toFixed(1)
                neutral.push(obj[key])
            }
            else if (key == "happy") {
                obj[key] = obj[key].toFixed(1)
                happy.push(obj[key])
            }
            else if (key == "sad") {
                obj[key] = obj[key].toFixed(1)
                sad.push(obj[key])

            }
            else if (key == "angry") {
                obj[key] = obj[key].toFixed(1)
                angry.push(obj[key])
            }
            else if (key == "fearful") {
                obj[key] = obj[key].toFixed(1)
                fearful.push(obj[key])
            }
            else if (key == "disgusted") {
                obj[key] = obj[key].toFixed(1)
                disgusted.push(obj[key])
            }
            else if (key == "surprised") {
                obj[key] = obj[key].toFixed(1)
                surprised.push(obj[key])
            }
            else if (key == "timeStamp") {
                timestamp.push(obj[key])
            }
           
                
        })
    }) 
    apexChart()
}

function apexChart() {

    var options = {
        chart: {
            type: 'line',
            height: '300px'
        },
        series: [{
                name: 'Neutral',
                data: neutral
            },
            {
                name: 'Happy',
                data: happy
            },
            {
                name: 'Sad',
                data: sad
            },
            {
                name: 'Angry',
                data: angry
            },
            {
                name: 'Fearful',
                data: fearful
            },
            {
                name: 'Disgusted',
                data: disgusted
            },
            {
                name: 'Surprised',
                data: surprised
        }],
        labels: timestamp,
        xaxis: {
            type: 'datetime',
            labels: {
                datetimeUTC: false
            }

        },
        tooltip: {
            x: {
                show: true,
                format: 'hh:mm:ss'
            }
        }

    }

    var options_pie = {
        chart: {
            type: 'pie',
            width: '100%'
        },
        series: counter,
        labels: ['neutral', 'happy', 'sad', 'angry', 'fearful', 'disgusted', 'surprised']
    }

    var chart = new ApexCharts(document.querySelector("#chart"), options);
    var chart_pie = new ApexCharts(document.querySelector("#chart2"), options_pie);

    chart.render();
    chart_pie.render();



    
}

