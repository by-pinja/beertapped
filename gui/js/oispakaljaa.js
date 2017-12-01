/**
 * Copyright 2017 Google Inc. All Rights Reserved.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

(function() {
    "use strict";

    var ENTER_KEY_CODE = 13;
    var queryInput, resultDiv, sendButton, tuoppi;
    var accessToken = "6bf0429a1bb24bb9a04ebad4066dfcc0";

    window.onload = init;

    function init() {
        queryInput = document.getElementById("q");
        sendButton = document.getElementById("sendButton");
        resultDiv = document.getElementById("result");
        tuoppi = document.getElementById("tuoppi-wrapper");
        window.init(accessToken);
        queryInput.addEventListener("keydown", queryInputKeyDown);
        sendButton.addEventListener("click", sendButtonClick);
        tuoppi.addEventListener("click", kubla);

        setTimeout(function () {
            setResponseOnNode("Well h..");

            setTimeout(function () {
                createPictureTaker();
            }, 2500);

        }, 4000);

    }

    function createPictureTaker(response) { // TODO: Tätä tuskin tarttee näin vaikeesti ekassa...
        var bubbleContainer = document.getElementById("bubbles");
        bubbleContainer.innerHTML +=
            "<div class=\"bubble-wrap\">\n" +
            "<div class=\"bubble right\">\n" +
            "Sorry, my voice was a bit down! Please start talking to me..." +
            "<div id=\"take_a_picture\" class=\"picture-btn\" href=\"#\" rel=\"modal:open\">OR TAKE A PICTURE</div>" +
            "</div>\n" +
            "</div>";

        response = "Sorry, my voice was a bit down! Please start talking to me... Or take a picture";
        scrollPosition();

        document.getElementById("suu").style.display = "none";

        document.getElementById("suu-auki").style.display = "block";

        var msg = new SpeechSynthesisUtterance(response);

        var voices = speechSynthesis.getVoices();

        for(var i = 0; i < voices.length ; i++) {
            if (voices[i].lang == 'fi-FI') {
                msg.voice = voices[i];
            }
        }

        window.speechSynthesis.speak(msg);

        msg.onend = function(event) { // ei toimi? tilalle timeout
            document.getElementById("suu-auki").style.display = "none";
            document.getElementById("suu").style.display = "block";
        }

        setTimeout(function () {
            document.getElementById("suu-auki").style.display = "none";
            document.getElementById("suu").style.display = "block";
            }, 4750);
    }



    function scrollPosition() {
        var elem = document.getElementById('bubbles');
        elem.scrollTop = elem.scrollHeight;
    }

    function queryInputKeyDown(event) {
        if (event.which !== ENTER_KEY_CODE) {
            return;
        }
        messageSend();
    }

    function sendButtonClick(event) {
        messageSend();
    }



    function createQueryNode(query) {
        var bubbleContainer = document.getElementById("bubbles");
        bubbleContainer.innerHTML +=
            "<div class=\"bubble-wrap\">\n" +
            "<div class=\"headbubble\"><i class=\"material-icons\">face</i></div>" +
            "<div class=\"bubble left\">\n" +
            query +
            "</div>\n" +
            "</div>";

        scrollPosition();
    }

    function setResponseOnNode(response) {
        setTimeout(function () {
            var bubbleContainer = document.getElementById("bubbles");
            bubbleContainer.innerHTML +=
                "<div class=\"bubble-wrap\">\n" +
                "<div class=\"bubble right\">\n" +
                response +
                "</div>\n" +
                "</div>";

            scrollPosition();

            document.getElementById("suu").style.display = "none";
            document.getElementById("suu-alku").style.display = "none";
            document.getElementById("suu-auki").style.display = "block";

            var msg = new SpeechSynthesisUtterance(response);

            var voices = speechSynthesis.getVoices();

            for(var i = 0; i < voices.length ; i++) {
                if (voices[i].lang == 'fi-FI') {
                    msg.voice = voices[i];
                }
            }

            window.speechSynthesis.speak(msg);

            msg.onend = function(event) {
                document.getElementById("suu-auki").style.display = "none";
                document.getElementById("suu").style.display = "block";
            }
        }, 600);
    }

    function setResponseJSON(response) {
        console.log(response);

        if (response.result.action === "username" && response.result.parameters.username != null && response.result.parameters.username.length !== 0) {
            // Make api call
            console.log("Lets call");

            var url = "https://beertapped-master.protacon.cloud/api/bestBeer/" + response.result.parameters.username;

            setTimeout(function () {
                setResponseOnNode("Hmmmmmm...");

                $.get(url, function (data) {
                    setTimeout(function () {
                        console.log(data);
                        setResponseOnNode("May I recommend " + data.andTheWinnerIs);
                    }, 2000);
                });
            }, 7000);
        }
    }

    function sendRequest() {

    }

    function messageSend() {
        var value = queryInput.value;
        queryInput.value = "";

        if (value.trim() == '') return; // Skip empty requests

        createQueryNode(value);

        sendText(value)
            .then(function(response) {
                var result;
                try {
                    result = response.result.fulfillment.speech
                } catch(error) {
                    result = "";
                }
                setResponseJSON(response);
                setResponseOnNode(result);
            })
            .catch(function(err) {
                setResponseJSON(err);
                setResponseOnNode("Something goes wrong. This is bad. I need a beer.");
            });
    }

    function kubla(event) {
        document.getElementById("background-wrap").style.visibility = "visible";
        setResponseOnNode("Beer is good, race was bad. Black round Pirelli. Have some bubbles, hoepoenassu.");
    }

})();

