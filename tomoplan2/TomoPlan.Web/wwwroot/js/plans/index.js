import { goToTomorrow, shouldDisplayTomorrow, initPlanNavigator } from "./planNavigator.js";
import { renderGrid} from "./planGrid.js";

document.addEventListener("DOMContentLoaded", function () {
    if (shouldDisplayTomorrow()) {
        goToTomorrow();
        return;
    }
    initPlanNavigator();
    renderGrid();
});