document.addEventListener("DOMContentLoaded", function() {
    const currentDate = document.getElementById("currentDate").innerText;
    const nextDayBtn = document.getElementById("nextDayBtn");
    const prevDayBtn = document.getElementById("prevDayBtn");
    const delTaskBtns = document.querySelectorAll('[id^="delTaskBtn_"]');
    let selectedTaskId = null;
    const delTaskModal = new bootstrap.Modal('#delTaskModal', {});
    const submitDeleteBtn = document.getElementById("submitDelBtn");
    const delTaskForm = document.getElementById("delTaskForm");

    nextDayBtn.addEventListener("click", function(event) {
        event.preventDefault();

        const nextDay = new Date(currentDate);
        nextDay.setDate(nextDay.getDate() + 1);

        const formatted = nextDay.toISOString().split("T")[0];

        const url = new URL(location.origin);
        url.searchParams.set("date", formatted);

        window.location.href = url.toString();
    });

    prevDayBtn.addEventListener("click", function(event) {
        event.preventDefault();

        const nextDay = new Date(currentDate);
        nextDay.setDate(nextDay.getDate() - 1);

        const formatted = nextDay.toISOString().split("T")[0];

        const url = new URL(location.origin);
        url.searchParams.set("date", formatted);
        url.trim

        window.location.href = url.toString();
    });

    delTaskBtns.forEach(function(delTaskBtn) {
        const taskId = delTaskBtn.id.split("_")[1];
        delTaskBtn.addEventListener("click", function(e) {
            selectedTaskId = taskId;
            delTaskModal.show();
        });
    });

    submitDeleteBtn.addEventListener("click", function(event) {
        event.preventDefault();

        const taskId = selectedTaskId;
        delTaskForm.action += `&taskId=${taskId}`

        delTaskForm.submit();
    });
});