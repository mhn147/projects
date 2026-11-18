function getHoursFormatted(locale = undefined) {
  const hours = [];
  const formatter = new Intl.DateTimeFormat(locale, {
    hour: "numeric",
    hour12: undefined, // Let the locale decide
  });

  for (let i = 0; i <= 24; i++) {
    const date = new Date();
    date.setHours(i, 0, 0, 0);
    const formatted = formatter.format(date);
    hours.push(formatted);
  }

  return hours;
}

function renderHours() {
  const hoursFormatted = getHoursFormatted();
  const hourElements = document.getElementsByClassName("hour-label");
  for (let i = 0; i < hourElements.length; i++) {
    hourElements[i].innerHTML = hoursFormatted[i];
  }
}

function updateCursor() {
  const el = document.getElementById("grid-cursor");
  const now = new Date();
  const minutes = now.getHours() * 60 + now.getMinutes();
  const hourHeight = 56;
  el.style.top = (minutes / 60) * hourHeight + "px";
}

function updateAndScrollToCursor() {
  updateCursor();

  const el = document.getElementById("grid-cursor");
  el.scrollIntoView({ behavior: "smooth", block: "center" });

  setInterval(
    () => {
      updateCursor();
    },
    60 * 5 * 1000,
  ); // updat eevery 5min
}

export { renderHours, updateAndScrollToCursor };
