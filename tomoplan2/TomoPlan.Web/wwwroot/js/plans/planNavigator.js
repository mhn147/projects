function shouldDisplayTomorrow() {
  const params = new URLSearchParams(location.search);
  if (!params.has("date")) {
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
  const startOfDay = (x) =>
    new Date(x.getFullYear(), x.getMonth(), x.getDate());
  const diffDays = Math.round(
    (startOfDay(planDate) - startOfDay(new Date())) / 86_400_000,
  );

  if (diffDays === 0) return "Today";
  if (diffDays === 1) return "Tomorrow";
  return planDate.toLocaleDateString(undefined, { weekday: "long" });
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
