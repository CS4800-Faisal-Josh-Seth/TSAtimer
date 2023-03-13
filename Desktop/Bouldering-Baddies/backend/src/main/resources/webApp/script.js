var timerBtn = document.getElementById("timerBtn");
var timerDisplay = document.getElementById("timerDisplay");
var startTime, elapsedTime, timerInterval;

function startTimer() {
	startTime = new Date().getTime();
	timerInterval = setInterval(updateTimer, 10);
	timerBtn.removeEventListener("click", startTimer);
	timerBtn.addEventListener("click", stopTimer);
	timerBtn.textContent = "Stop Timer";
}

function stopTimer() {
	clearInterval(timerInterval);
	elapsedTime = new Date().getTime() - startTime;
	var hours = Math.floor(elapsedTime / 3600000);
	var minutes = Math.floor((elapsedTime - hours * 3600000) / 60000);
	var seconds = Math.floor((elapsedTime - hours * 3600000 - minutes * 60000) / 1000);
	var milliseconds = elapsedTime - hours * 3600000 - minutes * 60000 - seconds * 1000;
	timerDisplay.textContent = ("0" + hours).slice(-2) + ":" + ("0" + minutes).slice(-2) + ":" + ("0" + seconds).slice(-2) + "." + ("00" + milliseconds).slice(-3);
	timerBtn.removeEventListener("click", stopTimer);
	timerBtn.addEventListener("click", startTimer);
	timerBtn.textContent = "Start Timer";
}

function updateTimer() {
	elapsedTime = new Date().getTime() - startTime;
	var hours = Math.floor(elapsedTime / 3600000);
	var minutes = Math.floor((elapsedTime - hours * 3600000) / 60000);
	var seconds = Math.floor((elapsedTime - hours * 3600000 - minutes * 60000) / 1000);
	var milliseconds = elapsedTime - hours * 3600000 - minutes * 60000 - seconds * 1000;
	timerDisplay.textContent = ("0" + hours).slice(-2) + ":" + ("0" + minutes).slice(-2) + ":" + ("0" + seconds).slice(-2) + "." + ("00" + milliseconds).slice(-3);
}

timerBtn.addEventListener("click", startTimer);
