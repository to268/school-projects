const btnOpenChatbot = document.querySelector('#open-chatbot');
const chatbotDiv = document.querySelector('#chatbot-div');
const chatbotDialog = document.querySelector('.chatbot-dialog');
const topic = get_topic();

console.log(topic);

fetch(ROOT_URL + '/api/chatbot/questions?topic=' + topic, {
    method: 'GET',
    headers: {
        'Accept': 'application/json',
    },
})
.then(response => response.text())
.then((text) => {
    const questions = JSON.parse(text);

    questions.forEach(question => {
        create_question(question.question, question.answer);
    });

    // Always display this question
    create_question('Plus de questions ?', '<a href="' + ROOT_URL + '/guide">guide utilisateur</a>');

    btnOpenChatbot.addEventListener("click", () => {
        chatbotDiv.classList.toggle("hidden-chatbot");
    });
});

function get_topic() {
    const topic = window.location.pathname.split('/')[1];

    if (!topic)
        return 'home';

    return topic
}

function create_question(question, answer) {
    const details = document.createElement('details');
    const summary = document.createElement('summary');

    summary.innerText = question;
    details.innerHTML = answer;

    details.append(summary);
    chatbotDialog.append(details);
}
