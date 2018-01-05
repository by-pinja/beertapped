navigator.getUserMedia({video: true}, function(stream) {
        var vvideo = document.getElementById("v");
		var vcanvas = document.getElementById("c");
		var vbutton = document.getElementById("b");
        var vwidth = vvideo.offsetWidth
        , vheight = vvideo.offsetHeight;
		vvideo.srcObject = stream;
        vvideo.play();
		vbutton.onclick = function() {
            /*if (!localStorage.aws_access_key_id || !localStorage.aws_secret_access_key) {
                console.error("Please, configure the AWS credentials to use this feature");
                return;
            }*/
            $("#u").addClass("showMe");
			vcanvas.getContext("2d").drawImage(vvideo, 0, 0, vwidth, vheight);
			var img = vcanvas.toDataURL("image/png");
            console.log(img);
			vcanvas.appendChild(img);
		};
	}, function(err) {});

function openPictureModal() {
    console.log("nappia painettu")
    $("#video_container").addClass("showMe");
    return false;
}

/*
$("#take_a_picture").click(function() {
  $("#video_container").addClass("showMe");
  return false;
});
*/

$("#d").click(function() {
  $("#u").removeClass("showMe");
  $("#video_container").removeClass("showMe");
  return false;
});