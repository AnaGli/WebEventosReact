const apiUrl = "https://localhost:44319/api/";
const myHeaders = new Headers();
myHeaders.append("content-type", "application/json");

function createOptions(object) {
    return {
        mode: "cors",
        method: "POST",
        body: JSON.stringify(object),
        headers: myHeaders,
    };
}

export async function listEvent() {
    return await fetch(apiUrl + "Admin/listar")
        .then((resp) => resp.json());
}

export async function listCategories() {
    return await fetch(apiUrl + "Admin/listarCategorias")
        .then((response) => response.json());
}

export async function listEventByCategory(categoryId) {
    return await fetch(apiUrl + "Admin/eventos/categoria/" + categoryId.toString())
        .then((resp) => resp.json());
}
export async function listParticipantByEvent(eventId) {
    return await fetch(apiUrl + "Admin/participantes/" + eventId.toString()).then((response) => response.json());
}


export async function userSignup(eventId, username) {
    const user = {
        idEvento: parseInt(eventId),
        loginParticipante: username,
    };

    const options = createOptions(user);

    return await fetch(apiUrl + "Usuario/criar", options).then((response) => response.json());
}

export async function createEvent(eventName, startDatetime, endDatetime, place, description, maxSeats, idEventCategory) {
    const event = {
        "nome": eventName,
        "dataHoraInicio": startDatetime,
        "dataHoraFim": endDatetime,
        "local": place,
        "descricao": description,
        "limiteVagas": parseInt(maxSeats),
        "idCategoriaEvento": parseInt(idEventCategory)

    };

    const options = createOptions(event);

    return await fetch(apiUrl + "Admin/criar", options).then((response) => response.json());
}

export async function getEventDetails(eventId) {
    return await fetch(apiUrl + "Admin/detalhes/" + eventId.toString()).then((response) => response.json());
}
