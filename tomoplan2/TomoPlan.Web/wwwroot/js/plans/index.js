import {
  goToTomorrow,
  shouldDisplayTomorrow,
  initPlanNavigator,
} from "./planNavigator.js";
import { renderHours, updateAndScrollToCursor } from "./planGrid.js";

function registerHourRowEvents() {
  const modalEl = document.getElementById("addTaskModal");
  const modal = bootstrap.Modal.getOrCreateInstance(modalEl);
  document.querySelectorAll('[id^="hour-row-"]').forEach((el) => {
    el.addEventListener("dblclick", () => {
      const hour = parseInt(el.id.slice("hour-row-".length), 10);
      const formattedTimeStart = `${String(hour).padStart(2, "0")}:00`;
      const formattedTimeEnd = `${String(hour).padStart(2, "0")}:30`;
      document.getElementById("startTimeInput").value = formattedTimeStart;
      document.getElementById("endTimeInput").value = formattedTimeEnd;
      modal.show();
    });
  });
}

function registerAddTaskFormEvents() {
  document
    .getElementById("confirmAddTaskBtn")
    .addEventListener("click", function () {
      const formElm = document.getElementById("addTaskForm");

      if (!formElm.reportValidity()) {
        return;
      }

      formElm.submit();
    });
}

function registerEvents() {
  registerAddTaskFormEvents();
  registerHourRowEvents();
}

document.addEventListener("DOMContentLoaded", function () {
  if (shouldDisplayTomorrow()) {
    goToTomorrow();
    return;
  }
  initPlanNavigator();
  renderHours();

  registerEvents();
  updateAndScrollToCursor();
});
