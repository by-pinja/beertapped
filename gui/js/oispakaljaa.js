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
    var queryInput, resultDiv, sendButton;
    var accessToken = "6bf0429a1bb24bb9a04ebad4066dfcc0";

    window.onload = init;

    function init() {
        queryInput = document.getElementById("q");
        sendButton = document.getElementById("sendButton");
        resultDiv = document.getElementById("result");
        window.init(accessToken);
        queryInput.addEventListener("keydown", queryInputKeyDown);
        sendButton.addEventListener("click", sendButtonClick);

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
        /*var node = document.createElement('div');
        node.className = "clearfix left-align left card-panel green accent-1";
        node.innerHTML = query;
        resultDiv.appendChild(node); */

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
        var bubbleContainer = document.getElementById("bubbles");
        bubbleContainer.innerHTML +=
            "<div class=\"bubble-wrap\">\n" +
            "      <div class=\"bubble right\">\n" +
            response +
            "</div>\n" +
            "</div>";

        scrollPosition();

        document.getElementById("suu").className = "suu-auki";

        var msg = new SpeechSynthesisUtterance(response);
        window.speechSynthesis.speak(msg);

        msg.onend = function(event) {
            document.getElementById("suu").className = 'suu';
        }
    }

    function setResponseJSON(response) {
        console.log(response);
       /* var bubbleContainer = document.getElementById("bubbles");
        bubbleContainer.innerHTML +=
            "<div class=\"bubble-wrap\">\n" +
            "      <div class=\"bubble right\">\n" +
            response.result.fulfillment.speech +
            "      </div>\n" +
            "</div>"*/
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
                setResponseOnNode("Something goes wrong");
            });
    }

})();
