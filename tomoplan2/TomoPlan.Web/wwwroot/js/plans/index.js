import { goToTomorrow, shouldDisplayTomorrow, initPlanNavigator } from "./planNavigator.js";
import { renderGrid, renderHours } from "./planGrid.js";

document.addEventListener("DOMContentLoaded", function () {
    if (shouldDisplayTomorrow()) {
        goToTomorrow();
        return;
    }
    initPlanNavigator();
    //renderGrid();
    renderHours();
    
    // TODO: refactor
    document.getElementById("confirmAddTaskBtn").addEventListener("click", function () {
        const formElm = document.getElementById("addTaskForm");
            
        if (!formElm.reportValidity()) {
            return;
        }
        
        formElm.submit();
    });
});