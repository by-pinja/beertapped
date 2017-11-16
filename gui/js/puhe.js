    var recognizing = false;
    var recognition = new webkitSpeechRecognition();
    recognition.continuous = true;
    recognition.interimResults = true;
    recognition.onstart = function() {
        recognizing = true;
    };
    recognition.onerror = function(event) {
        console.log("Ei ollu kaljaa", event);
    };
    recognition.onend = function() {
        recognizing = false;
        document.getElementById("startButton").classList.remove('mikki-rec');
        document.getElementById("startButton").classList.add('mikki-off');
    };
    recognition.onresult = function(event) {
        var interim_transcript = '';
        var final_transcript = '';
        for (var i = event.resultIndex; i < event.results.length; ++i) {
            if (event.results[i].isFinal) {
                final_transcript += event.results[i][0].transcript;
            } else {
                interim_transcript += event.results[i][0].transcript;
            }
        }
        console.log(final_transcript);
        document.getElementById('q').value = final_transcript;
    };

function startButton(event) {
    //alert(recognizing);
    if (recognizing) {
        recognition.stop();
        return;
    }
    recognition.lang = 'fi-FI';
    recognition.start();
    document.getElementById("startButton").classList.remove('mikki-off');
    document.getElementById("startButton").classList.add('mikki-rec');
}