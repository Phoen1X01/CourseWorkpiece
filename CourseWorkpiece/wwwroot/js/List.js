function onRender() {
    filterType();
    filterText();

    const trafficElements = document.querySelectorAll(".traffic");

    trafficElements.forEach((element) => {
        let filterTypeElement = element.getAttribute('filterType') === "true"
        let filterTextElement = element.getAttribute('filterText') === "true"

        let res = filterTypeElement || filterTextElement;
        if (res) {
            element.style.display = "None";
        } else {
            element.style.display = "";
        }

    });
}

var typeFilter = -1;
function onFilterType(filter) {
    typeFilter = filter;
    onRender();
}

function filterType() {
    const trafficElements = document.querySelectorAll(".traffic");

    trafficElements.forEach((element) => {
        if (typeFilter == -1) {
            element.setAttribute('filterType', false)
            return;
        }

        const inputs = element.querySelectorAll("input");
        if (inputs[typeFilter].checked) {
            element.setAttribute('filterType', false)
        } else {
            element.setAttribute('filterType', true)
        }
    });
}

function onFilterText() {
    onRender();
}

function filterText() {
    const input = document.getElementById('searchInput');
    const inputText = input.value;
    const trafficElements = document.querySelectorAll(".traffic");

    trafficElements.forEach((element) => {


        const inputs = element.querySelectorAll("th");
        const text = inputs[0].textContent;

        const res = castomLevenshteinDistance(inputText, text);
        console.log(res);
        if (res<2) {
            element.setAttribute('filterText', false)
        } else {
            element.setAttribute('filterText', true)
        }
    });
}

function onCheckboxChange(checkbox, id_chec, id) {
    if (checkbox.checked) {
        console.log("Checkbox is checked " + id_chec + "  " + id);

        for (let i = 1; i <= 4; i++) {
            if (i == id_chec) continue;
            var checkbox = document.getElementById(`list-${i}-${id}`);
            checkbox.checked = false;
        }

        onRender();

        const url = "/List/EditorAttendance";
        const data = {
            id: id,
            traffic: id_chec,
        };
        fetch(url, {
            method: "PUT",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(data),
        })
            .then((response) => response.json())
            .then((data) => console.log("Success:", data))
            .catch((error) => console.error("Error:", error));
    } else {
        checkbox.checked = true;
    }
}


async function siteData() {
    const calendarInput = document.getElementById('datepicker');
    let data = calendarInput.getAttribute('data-date');
    const currentUrl = window.location.href;


    const url = new URL(currentUrl);

    if (data == null) {
        //url.searchParams.delete('day');
        //url.searchParams.delete('month');
        //url.searchParams.delete('year');
    } else {
        data = data.split('/');
        url.searchParams.set('day', data[0]);
        url.searchParams.set('month', data[1]);
        url.searchParams.set('year', data[2]);
    }

    const paraInput = document.getElementById('lessonNumber');

    url.searchParams.set('pair', paraInput.value);


    history.pushState(null, '', url);
    location.reload();
}




function castomLevenshteinDistance(a, b) {
    const sliseA = a.split(' ');
    const sliseB = b.split(' ');

    let res = 0;

    for (let a_i = 0; a_i < sliseA.length; a_i++) {
        let v = Number.MAX_VALUE;
        for (let b_i = 0; b_i < sliseB.length; b_i++) {
            let new_a = sliseA[a_i];
            let new_b = sliseB[b_i];
            const new_len = new_a.length < new_b.length ? new_a.length : new_b.length;
            new_a = new_a.substring(0, new_len);
            new_b = new_b.substring(0, new_len);

            const v_new = levenshteinDistance(new_a, new_b);
            if (v_new < v)
                v = v_new;
        }
        res += v;
    }
    res /= sliseA.length;
    return res;
}

function levenshteinDistance(a, b) {
    const matrix = [];

    // Инициализация матрицы
    for (let i = 0; i <= b.length; i++) {
        matrix[i] = [i];
    }
    for (let j = 0; j <= a.length; j++) {
        matrix[0][j] = j;
    }

    // Вычисление расстояний
    for (let i = 1; i <= b.length; i++) {
        for (let j = 1; j <= a.length; j++) {
            if (b.charAt(i - 1) === a.charAt(j - 1)) {
                matrix[i][j] = matrix[i - 1][j - 1];
            } else {
                matrix[i][j] = Math.min(
                    matrix[i - 1][j - 1] + 1, // замена
                    matrix[i][j - 1] + 1,     // вставка
                    matrix[i - 1][j] + 1      // удаление
                );
            }
        }
    }

    return matrix[b.length][a.length];
}