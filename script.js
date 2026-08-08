const fortunes = [
    "Your code will work on the first try (maybe)",
    "A bug is just an undocumented feature",
    "Coffee will fix this issue",
    "Just Another Try",
    "Great success is coming after this console.log."
];

const btn = document.getElementById('fortuneBtn');
const fortuneText = document.getElementById('fortuneText');
btn.addEventListener('click', function() {
    const randomIndex = Math.floor(Math.random() * fortunes.length);
        fortuneText.textContent = fortunes[randomIndex];
});