const launchBtn = document.getElementById('launchBtn');
const rocket = document.getElementById('flying-saucer');
launchBtn.addEventListener('click', function() {
    rocket.classList.add('fly');
});