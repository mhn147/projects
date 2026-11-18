const planDate = document.planDate;

function shouldDisplayTomorrow() {
  const now = new Date();
  const sameDay =
    now.getUTCFullYear() === planDate.getUTCFullYear() &&
    now.getUTCMonth() === planDate.getUTCMonth() &&
    now.getUTCDate() === planDate.getUTCDate();

  if (!sameDay) {
    return false;
  }

  return new Date().getHours() >= 22;
}

function goToTomorrow() {
  const dateObj = new Date(planDate);
  dateObj.setUTCDate(dateObj.getUTCDate() + 1);
  window.location.href = `/?date=${dateObj.toISOString().slice(0, 10)}`;
}

function getDateLabel() {
  const now = new Date();

  const todayUtc = Date.UTC(now.getFullYear(), now.getMonth(), now.getDate());
  const diffDays = Math.round((planDate.getTime() - todayUtc) / 86_400_000);

  if (diffDays === 0) return "Today";
  if (diffDays === 1) return "Tomorrow";

  return planDate.toLocaleDateString(undefined, {
    weekday: "long",
    timeZone: "UTC",
  });
}

function initPlanNavigator() {
  const dateLabel = document.getElementById("dateLabel");
  dateLabel.innerText = planDate.toLocaleDateString(undefined, {
    dateStyle: "medium",
  });
  dateLabel.style.display = "block";

  const dayLabel = document.getElementById("dayLabel");
  dayLabel.innerText = getDateLabel();
  dayLabel.style.display = "block";

  const nextDayBtn = document.getElementById("nextDay");
  nextDayBtn.addEventListener("click", function () {
    const y = new Date(planDate);
    y.setUTCDate(y.getUTCDate() + 1);
    window.location.href = `/?date=${y.toISOString().slice(0, 10)}`;
  });

  const prevDayBtn = document.getElementById("prevDay");
  prevDayBtn.addEventListener("click", function () {
    const y = new Date(planDate);
    y.setUTCDate(y.getUTCDate() - 1);
    window.location.href = `/?date=${y.toISOString().slice(0, 10)}`;
  });
}

export { shouldDisplayTomorrow, goToTomorrow, initPlanNavigator };
