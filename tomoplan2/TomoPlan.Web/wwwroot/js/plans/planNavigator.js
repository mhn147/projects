const planDate = document.planDate;

function shouldDisplayTomorrow() {
    return (new Date()).getHours() >= 22;
}

function goToTomorrow() {
    const dateObj = new Date(planDate);
    dateObj.setUTCDate(y.getUTCDate() + 1)
    window.location.href = `/?date=${dateObj.toISOString()}`;
}

function getDateLabel() {
    const now = new Date();

    const sameToday =
        now.getUTCFullYear() === planDate.getUTCFullYear() &&
        now.getUTCMonth() === planDate.getUTCMonth() &&
        now.getUTCDate() === planDate.getUTCDate();
    
    if (sameToday) {
        return "Today";
    }
    
    const tomo = new Date(now);
    tomo.setUTCDate(now.getUTCDate() + 1);
    
    const sameTomo =
        tomo.getUTCFullYear() === planDate.getUTCFullYear() &&
        tomo.getUTCMonth() === planDate.getUTCMonth() &&
        tomo.getUTCDate() === planDate.getUTCDate();
    
    if (sameTomo) {
        return "Tomorrow";
    }

    return planDate.toLocaleDateString(undefined, {
        weekday: "long"
    });
}

function initPlanNavigator() {

    const dateLabel = document.getElementById("dateLabel");
    dateLabel.innerText = new Intl.DateTimeFormat(undefined, {
        dateStyle: "medium"
    }).format(planDate);
    dateLabel.style.display = "block";

    const dayLabel = document.getElementById("dayLabel");
    dayLabel.innerText = getDateLabel();
    dayLabel.style.display = "block";

    const nextDayBtn = document.getElementById("nextDay");
    nextDayBtn.addEventListener("click", function () {
        const y = new Date(planDate);
        y.setUTCDate(y.getUTCDate() + 1)
        window.location.href = `/?date=${y.toISOString()}`;
    });

    const prevDayBtn = document.getElementById("prevDay");
    prevDayBtn.addEventListener("click", function () {
        const y = new Date(planDate);
        y.setUTCDate(y.getUTCDate() - 1)
        window.location.href = `/?date=${y.toISOString()}`;
    })
}

export { shouldDisplayTomorrow, goToTomorrow, initPlanNavigator };