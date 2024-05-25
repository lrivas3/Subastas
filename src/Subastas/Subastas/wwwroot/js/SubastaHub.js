const connection = new signalR.HubConnectionBuilder().withUrl("/SubastaHub").build();

connection.on("ReceiveBid", function (user, amount) {
    // Actualiza la lista de pujas y el monto actual
    const bidTable = document.querySelector("#bid-table-body");
    const newRow = document.createElement("tr");
    newRow.innerHTML = `<td>${user}</td><td>${amount}</td><td>${new Date().toLocaleString()}</td>`;
    bidTable.prepend(newRow);

    // Actualiza el monto actual
    document.querySelector("#current-amount").innerText = `$${amount}`;
});

connection.on("ReceiveMessage", function (user, message) {
    // Actualiza el chat
    const chatDiv = document.querySelector("#chat-messages");
    const newMessage = document.createElement("div");
    newMessage.className = "message";
    newMessage.innerHTML = `<strong>${user}:</strong> ${message}`;
    chatDiv.appendChild(newMessage);
});

connection.on("UpdateParticipants", function (participants) {
    // Actualiza la lista de participantes
    const participantsTable = document.querySelector("#participants-table-body");
    participantsTable.innerHTML = "";
    participants.forEach(participant => {
        const newRow = document.createElement("tr");
        newRow.innerHTML = `<td>${participant.nombreUsuario}</td><td>${participant.correoUsuario}</td><td>${participant.fechaSubasta}</td>`;
        participantsTable.appendChild(newRow);
    });
});

connection.start().catch(function (err) {
    return console.error(err.toString());
});

// Ejemplo de cómo enviar pujas y mensajes
//document.querySelector(".btn-primary").addEventListener("click", function (event) {
//    const amount = parseFloat(prompt("Enter bid amount:"));
//    const user = currentUser;
//    connection.invoke("SendBid", user, amount).catch(function (err) {
//        return console.error(err.toString());
//    });
//    event.preventDefault();
//});

document.querySelector(".btn-send-message").addEventListener("click", function (event) {
    const message = document.querySelector("#chat-input").value;
    const user = currentUser;
    connection.invoke("SendMessage", user, message).catch(function (err) {
        return console.error(err.toString());
    });
    document.querySelector("#chat-input").value = "";
    event.preventDefault();
});

connection.on("ReceiveParticipants", function (participants) {
    const participantsTable = document.querySelector("#participants-table-body");
    participantsTable.innerHTML = '';
    participants.forEach(participant => {
        const newRow = document.createElement("tr");
        newRow.innerHTML = `<td>${participant.NombreUsuario}</td><td>${participant.CorreoUsuario}</td><td>${participant.FechaSubasta}</td>`;
        participantsTable.appendChild(newRow);
    });
});

window.addEventListener("beforeunload", function () {
    connection.invoke("UpdateParticipants", currentUser, false).catch(function (err) {
        return console.error(err.toString());
    });
});
