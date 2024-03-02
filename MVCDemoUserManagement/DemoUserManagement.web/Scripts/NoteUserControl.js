document.addEventListener("DOMContentLoaded", function () {
    // Add event listener for Add Note button
    document.getElementById("btnAddNote").addEventListener("click", addNote);

    // Fetch notes on page load
    fetchNotes();
});

function fetchNotes() {
    // Fetch notes from API and populate the table
    // Make an AJAX request to the server-side API to retrieve notes data
    // Example:
    // fetch("api/notes")
    // .then(response => response.json())
    // .then(data => {
    //     populateTable(data);
    // })
    // .catch(error => console.error("Error fetching notes:", error));
    // Replace the above code with actual implementation
}

function populateTable(data) {
    // Populate the table with data
    var tableBody = document.querySelector("#GridViewDocuments tbody");
    tableBody.innerHTML = "";

    data.forEach(note => {
        var row = tableBody.insertRow();
        row.innerHTML = `<td>${note.NoteID}</td>
                         <td>${note.ObjectID}</td>
                         <td>${note.ObjectType}</td>
                         <td>${note.NoteText}</td>
                         <td>${note.TimeStampFormatted}</td>`;
    });
}

function addNote() {
    // Add note logic
    var noteText = document.getElementById("txtNote").value;

    // Make an AJAX request to the server-side API to add a new note
    // Example:
    // fetch("api/notes", {
    //     method: "POST",
    //     headers: {
    //         "Content-Type": "application/json"
    //     },
    //     body: JSON.stringify({ noteText: noteText })
    // })
    // .then(response => response.json())
    // .then(data => {
    //     // Note added successfully, refresh the table
    //     fetchNotes();
    // })
    // .catch(error => console.error("Error adding note:", error));
    // Replace the above code with actual implementation
}
