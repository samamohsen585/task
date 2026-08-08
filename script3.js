const timeInput = document.getElementById('timeInput');
const startBtn = document.getElementById('startBtn');
const display = document.getElementById('display');
const historyList = document.getElementById('historyList');
let countdownInterval;
startBtn.addEventListener('click', function() {
    let timeLeft = parseInt(timeInput.value);
    clearInterval(countdownInterval);
    display.classList.remove('times-up');
    display.textContent = timeLeft;

    countdownInterval = setInterval(() => {
        timeLeft--;

        if (timeLeft > 0) {
            display.textContent = timeLeft;
        } else {
            clearInterval(countdownInterval);
            display.textContent = "Time's up!";
            display.classList.add('times-up');
            addHistoryRecord(parseInt(timeInput.value));
        }
    }, 1000);
});
function addHistoryRecord(seconds) {
    const now = new Date();
    const timeString = now.toLocaleTimeString('en-US', { 
        hour: 'numeric', 
        minute: '2-digit', 
        second: '2-digit', 
        hour12: true 
    });
    const historyItem = document.createElement('div');
    historyItem.className = 'history-item';
    historyItem.textContent = `${seconds}s completed at ${timeString}`;
    historyList.prepend(historyItem);
}