// Über secure solution. NOT!
$("#save").click(function() {
  localStorage.aws_access_key_id = $("#access_key_id").val();
  localStorage.aws_secret_access_key = $("#secret_access_key").val();
  console.log("Success");
});