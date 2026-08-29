function setupThemeToggle() {
    const toggle = document.getElementById('theme-toggle');
    const sunIcon = document.getElementById('icon-sun');
    const moonIcon = document.getElementById('icon-moon');

    function updateIcon(theme) {
        sunIcon.style.display = theme === 'dark' ? 'none' : 'inline-block';
        moonIcon.style.display = theme === 'dark' ? 'inline-block' : 'none';
    }

    updateIcon(document.documentElement.getAttribute('data-bs-theme'));

    toggle.addEventListener('click', function () {
        const current = document.documentElement.getAttribute('data-bs-theme');
        const next = current === 'dark' ? 'light' : 'dark';
        document.documentElement.setAttribute('data-bs-theme', next);
        localStorage.setItem('theme', next);
        updateIcon(next);
    });

    window.matchMedia('(prefers-color-scheme: dark)').addEventListener('change', (e) => {
        if (!localStorage.getItem('theme')) {
            const theme = e.matches ? 'dark' : 'light';
            document.documentElement.setAttribute('data-bs-theme', theme);
            updateIcon(theme);
        }
    });
}

function renderGrid() {
    const HOUR_HEIGHT = 56;
    const START_HOUR = 0;
    const END_HOUR = 24;
    const BASE_DATE = new Date(2026, 5, 28);

    // In-memory store keyed by day offset from BASE_DATE (0 = "tomorrow" in this demo).
    // Replace this object with calls to your real backend (e.g. fetch('/api/tasks?date=...'))
    let tasksByOffset = {
        0: [
            { id: 1, title: 'Stand-up sync', start: '09:00', end: '09:30', category: 'work' },
            { id: 2, title: 'Review PR: auth refactor', start: '10:00', end: '11:30', category: 'work' },
            { id: 3, title: 'Gym session', start: '12:30', end: '13:30', category: 'personal' },
            { id: 4, title: 'Client escalation call', start: '14:00', end: '15:00', category: 'urgent' },
            { id: 5, title: 'Deep work: API design', start: '15:30', end: '17:30', category: 'work' },
            { id: 6, title: 'Pick up groceries', start: '18:00', end: '18:30', category: 'personal' }
        ],
        1: [
            { id: 7, title: 'Dentist appointment', start: '08:00', end: '09:00', category: 'personal' },
            { id: 8, title: 'Sprint planning', start: '10:00', end: '11:00', category: 'work' },
            { id: 9, title: 'Fix prod issue', start: '13:00', end: '14:30', category: 'urgent' }
        ],
        2: []
    };

    let offset = 0;
    let editingTaskId = null;
    let nextId = 100;

    function timeToHours(timeStr) {
        const parts = timeStr.split(':').map(Number);
        const h = parts[0];
        const m = parts[1];
        return h + m / 60;
    }

    function fmtDisplayTime(timeStr) {
        const parts = timeStr.split(':').map(Number);
        const h = parts[0];
        const m = parts[1];
        const period = h >= 12 ? 'PM' : 'AM';
        const displayHour = h % 12 === 0 ? 12 : h % 12;
        return displayHour + ':' + String(m).padStart(2, '0') + ' ' + period;
    }

    function fmtHourLabel(h) {
        if (h === 0 || h === 24) return '12 AM';
        if (h === 12) return '12 PM';
        return (h > 12 ? h - 12 : h) + (h >= 12 ? ' PM' : ' AM');
    }

    function dateForOffset(off) {
        const d = new Date(BASE_DATE);
        d.setDate(d.getDate() + off);
        return d;
    }

    function getDateLabel(off) {
        return dateForOffset(off).toLocaleDateString('en-US', { month: 'short', day: 'numeric', year: 'numeric' });
    }

    function getDayName(off) {
        if (off === 0) return 'Tomorrow';
        if (off === -1) return 'Today';
        return dateForOffset(off).toLocaleDateString('en-US', { weekday: 'long' });
    }

    function currentTasks() {
        if (!tasksByOffset[offset]) tasksByOffset[offset] = [];
        return tasksByOffset[offset];
    }

    // function renderHeader() {
    //     document.getElementById('dayLabel').textContent = getDayName(offset);
    //     document.getElementById('dateLabel').textContent = getDateLabel(offset);
    // }

    function renderGrid() {
        const grid = document.getElementById('grid');
        const totalHours = END_HOUR - START_HOUR;
        grid.style.height = (totalHours * HOUR_HEIGHT) + 'px';
        grid.innerHTML = '';

        for (let h = START_HOUR; h <= END_HOUR; h++) {
            const row = document.createElement('div');
            row.className = 'hour-row';
            row.style.top = ((h - START_HOUR) * HOUR_HEIGHT) + 'px';
            grid.appendChild(row);

            const label = document.createElement('div');
            label.className = 'hour-label';
            label.textContent = fmtHourLabel(h);
            label.style.top = ((h - START_HOUR) * HOUR_HEIGHT - 7) + 'px';
            grid.appendChild(label);
        }

        const spine = document.createElement('div');
        spine.className = 'spine';
        grid.appendChild(spine);

        const tasks = currentTasks();

        if (tasks.length === 0) {
            const empty = document.createElement('div');
            empty.className = 'empty-state';
            empty.textContent = 'No tasks yet for this day';
            grid.appendChild(empty);
            return;
        }

        tasks.forEach(function (task) {
            const startH = timeToHours(task.start);
            const endH = timeToHours(task.end);
            const top = (startH - START_HOUR) * HOUR_HEIGHT;
            // endH 15.25
            // start 14
            // 1.25 * 50px -2
            const height = Math.max((endH - startH) * HOUR_HEIGHT - 2, 22);

            const block = document.createElement('div');
            block.className = 'task-block ' + task.category;
            block.style.top = top + 'px';
            block.style.height = height + 'px';
            block.setAttribute('role', 'button');
            block.setAttribute('tabindex', '0');

            const titleEl = document.createElement('div');
            titleEl.className = 'task-title';
            titleEl.textContent = task.title;
            block.appendChild(titleEl);

            if (height > 32) {
                const timeEl = document.createElement('div');
                timeEl.className = 'task-time';
                timeEl.textContent = fmtDisplayTime(task.start) + ' to ' + fmtDisplayTime(task.end);
                block.appendChild(timeEl);
            }

            block.addEventListener('click', function () {
                //openModal(task);
            });

            grid.appendChild(block);
        });
    }

    function render() {
        // renderHeader();
        renderGrid();
    }

    // function openModal(task) {
    //     const overlay = document.getElementById('modalOverlay');
    //     const title = document.getElementById('modalTitle');
    //     const deleteBtn = document.getElementById('deleteTaskBtn');

    //     if (task) {
    //         editingTaskId = task.id;
    //         title.textContent = 'Edit task';
    //         document.getElementById('taskTitle').value = task.title;
    //         document.getElementById('taskStart').value = task.start;
    //         document.getElementById('taskEnd').value = task.end;
    //         document.getElementById('taskCategory').value = task.category;
    //         deleteBtn.classList.remove('hidden');
    //     } else {
    //         editingTaskId = null;
    //         title.textContent = 'Add task';
    //         document.getElementById('taskTitle').value = '';
    //         document.getElementById('taskStart').value = '09:00';
    //         document.getElementById('taskEnd').value = '10:00';
    //         document.getElementById('taskCategory').value = 'work';
    //         deleteBtn.classList.add('hidden');
    //     }

    //     overlay.classList.remove('hidden');
    // }

    // function closeModal() {
    //     document.getElementById('modalOverlay').classList.add('hidden');
    // }

    document.getElementById('prevDay').addEventListener('click', function () {
        offset -= 1;
        render();
    });

    document.getElementById('nextDay').addEventListener('click', function () {
        offset += 1;
        render();
    });

    document.getElementById('addTaskBtn').addEventListener('click', function () {
        openModal(null);
    });

    // document.getElementById('modalClose').addEventListener('click', closeModal);
    // document.getElementById('cancelBtn').addEventListener('click', closeModal);

    // document.getElementById('modalOverlay').addEventListener('click', function (e) {
    //     if (e.target === this) closeModal();
    // });

    // document.getElementById('deleteTaskBtn').addEventListener('click', function () {
    //     if (editingTaskId === null) return;
    //     const tasks = currentTasks();
    //     const idx = tasks.findIndex(function (t) { return t.id === editingTaskId; });
    //     if (idx !== -1) tasks.splice(idx, 1);
    //     closeModal();
    //     render();
    // });

    // document.getElementById('taskForm').addEventListener('submit', function (e) {
    //     e.preventDefault();

    //     const title = document.getElementById('taskTitle').value.trim();
    //     const start = document.getElementById('taskStart').value;
    //     const end = document.getElementById('taskEnd').value;
    //     const category = document.getElementById('taskCategory').value;

    //     if (!title || !start || !end) return;

    //     if (timeToHours(end) <= timeToHours(start)) {
    //         alert('End time must be after start time.');
    //         return;
    //     }

    //     const tasks = currentTasks();

    //     if (editingTaskId !== null) {
    //         const idx = tasks.findIndex(function (t) { return t.id === editingTaskId; });
    //         if (idx !== -1) {
    //             tasks[idx] = { id: editingTaskId, title: title, start: start, end: end, category: category };
    //         }
    //     } else {
    //         tasks.push({ id: nextId++, title: title, start: start, end: end, category: category });
    //     }

    //     closeModal();
    //     render();
    // });

    render();
};

document.addEventListener("DOMContentLoaded", function () {
    setupThemeToggle();
    renderGrid();
});