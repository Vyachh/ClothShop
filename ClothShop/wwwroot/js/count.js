const form = document.querySelector('#form')
const taskInput = document.querySelector('#taskInput')
const tasksList = document.querySelector('#tasksList')
const emptyList = document.querySelector('#emptyList')

let tasks = [];

checkEmptyList();

if (localStorage.getItem('tasks')) {
    tasks = JSON.parse(localStorage.getItem('tasks'))
    tasks.forEach(element => renderTask(element));
}

tasksList.addEventListener('click', plusNum)
tasksList.addEventListener('click', minusNum)
form.addEventListener('submit', addTask)
tasksList.addEventListener('click', deleteTask)

function addTask(event) {
    event.preventDefault();

    const taskText = taskInput.value;

    const newTask = {
        id: Date.now(),
        text: taskText,
        plusId: Date.now(),
        inputId: Date.now(),
        minusId: Date.now(),
    };

    tasks.push(newTask)
    renderTask(newTask)

    taskInput.value = ''
    taskInput.focus()

    checkEmptyList();

    saveToLocalStorage();
}
function deleteTask(event) {
    if (event.target.dataset.action !== 'delete') return

    const parentNode = event.target.closest('.list-group-item');

    const id = Number(parentNode.id)

    tasks = tasks.filter((task) => task.id !== id)

    parentNode.remove();
    checkEmptyList();
    saveToLocalStorage();
}
function renderTask(task) {
    const cssClass = task.done ? "task-title task-title--done" : "task-title"
    const taskHTML = `

<li id="${task.id}" class="list-group-item d-flex justify-content-between task-item">
    <span class="${cssClass}">${task.text}</span>
    <ul class="item-count" id="${task.id}">
        <li><button class="plusBtn" data-action="plus" id="${task.plusId}">+</button></li>
        <li><input value="0" class="numInput" id="${task.inputId}" type="number"> </input></li>
        <li><button class="minusBtn" data-action="minus" id="${task.minusId}">-</button></li>
    </ul>
    <div class="task-item__buttons">
        <button type="button" data-action="done" class="btn-action">
            <img src="./img/tick.svg" alt="Done" width="18" height="18">
        </button>
        <button type="button" data-action="delete" class="btn-action">
            <img src="./img/cross.svg" alt="Done" width="18" height="18">
        </button>
    </div>
</li>`
    tasksList.insertAdjacentHTML('beforeend', taskHTML)
}
function plusNum(event) {
    if (event.target.dataset.action !== 'plus') return;

    const parentNode = event.target.closest('.item-count')

    const id = Number(parentNode.id)
    const foundTasks = tasks.filter((task) => task.id === id)

    console.log(foundTasks);
    let input = parentNode.querySelector('.numInput')
    ++input.value

}
function minusNum(event) {
    if (event.target.dataset.action !== 'minus') return;

    const parentNode = event.target.closest('.item-count')

    const id = Number(parentNode.id)
    const foundTasks = tasks.filter((task) => task.id === id)

    console.log(foundTasks);
    let input = parentNode.querySelector('.numInput')
    if (input.value > 0)
        --input.value
}
