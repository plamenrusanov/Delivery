﻿
$(document).ready(function () {
    var x = document.getElementsByTagName('li');
    for (var i = 0; i < x.length; i++) {
        if (x[i].className === "btn btn-danger btn-lg order-li") {
            x[i].click();
            playMusic();
            var al = document.getElementById('alertt');
            al.style.display = "block";
            var alContent = document.getElementById('alert-content');
            alContent.innerHTML = `Има неприета поръчка!`;
        }
    }
});
Audio.prototype.play = (function (play) {
    return function () {
        var audio = this,
            args = arguments,
            promise = play.apply(audio, args);
        if (promise !== undefined) {
            promise.catch(_ => {
                // Autoplay was prevented. This is optional, but add a button to start playing.
                var el = document.createElement("button");
                el.innerHTML = "Play";
                el.addEventListener("click", function () { play.apply(audio, args); });
                this.parentNode.insertBefore(el, this.nextSibling)
            });
        }
    };
})(Audio.prototype.play);
function playMusic() {
    var audio = document.getElementById('audio');
    audio.play();
};
function stopMusic() {
    var audio = document.getElementById('audio'); if (audio) { audio.currentTime = 0; audio.pause(); }
};

var connection = null;
setupConnection = () => {
    connection = new signalR.HubConnectionBuilder().withUrl("/orderHub").withAutomaticReconnect().build();

    connection.on("OperatorNewOrder", function (id) {
        insertOrder(id);
        if (document.getElementById(`li${id}`)) {
            document.getElementById(`li${id}`).click();
            playMusic();
            var al = document.getElementById('alertt');
            al.style.display = "block";
            var alContent = document.getElementById('alert-content');
            alContent.innerHTML = `Има неприета поръчка!`;

        }
    });

    connection.on("OperatorAlertMessage", function (message) { alert(message); });

    connection.on("OperatorStatusChanged", function (order, status, userId) {

        var li = document.getElementById(`li${order}`);
        if (status === "Unprocessed") {
            li.className = "btn btn-danger btn-lg order-li";
        } else if (status === "Processed") {
            li.className = "btn btn-warning btn-lg";
        } else if (status === "OnDelivery") {
            li.className = "btn btn-success btn-lg";
        } else if (status === "Delivered") {
            li.className = "btn btn-info btn-lg";
        }
        orderDetails(order);
        closeAlert();
    });

    connection.on("OperatorSetAlarm", function (order) {
        if (document.getElementById(`li${order}`).className == "btn btn-warning btn-lg") {
            document.getElementById(`li${order}`).click();
            var al = document.getElementById('alertt');
            al.style.display = "block";
            var alContent = document.getElementById('alert-content');
            alContent.innerHTML = `Поръчка номер ${order} трябва да пътува!`;
            playMusic();
        }
    });

    connection.on("OperatorFinished", function () { connection.stop(); });

    connection.start().catch(err => console.error(err.toString()));
};

setupConnection();

async function con() {
    if (connection.connectionState === "Disconnected") {
        var al = document.getElementById('alertt');
        al.style.display = "block";
        var alContent = document.getElementById('alert-content');
        alContent.innerHTML = `Няма връзка със сървъра!`;
        checkConnection();
        await setupConnection();
    }
}

setInterval(con, 10000);

function insertOrder(id) { var li = document.createElement("li"); li.className = "btn btn-danger btn-lg"; li.setAttribute("onclick", `orderDetails(${id})`); li.id = `li${id}`; li.style.width = "100%"; li.style.marginBottom = "2px"; var h5 = document.createElement("h5"); h5.textContent = `Поръчка: ${id}`; li.appendChild(h5); var list = document.getElementById("listOrders"); list.insertBefore(li, list.childNodes[0]); };

function cStatus(status, order, setTime) { connection.invoke("OperatorChangeStatus", status, order, setTime); };

function changeStatus(status) {
    var order = document.getElementById("order").innerHTML;
    var setTime;
    if (document.getElementById('theInput')) {
        setTime = document.getElementById('theInput').value;
    }
    cStatus(status, order, setTime);
};

function orderDetails(orderId) {
    $.ajax({
        url: `/Guest/Orders/Details?orderId=${orderId}`,
        cache: false,
        success: function (response) {
            document.getElementById("detailSection").innerHTML = response;
        }
    });
}

function minus() {
    var input = document.getElementById('theInput');
    if (input.value > 15) {
        input.value = parseInt(input.value, 10) - 5;
    }
}

function plus() {
    var input = document.getElementById('theInput');
    if (input.value < 100) {
        input.value = parseInt(input.value, 10) + 5;
    }
}

function displayDeliveryTax() {
    var el = document.getElementById(`btnDeliveryTax`);
    el.click();
}

function closeAlert() {
    var al = document.getElementById('alertt');
    if (al) {
        al.style.display = "none";
    }
    stopMusic();
}

function checkConnection() {
    setInterval(function () {
        if (connection.connectionState === "Connected") {
            window.location.reload();
        }
    }, 500);
}

audio.currentTime = 0;
audio.pause();


