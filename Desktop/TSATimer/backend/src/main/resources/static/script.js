let data;

function populateDropdown() {
  const dropdown = document.querySelector('#airport-select');

  fetch('http://ec2-18-119-130-187.us-east-2.compute.amazonaws.com:8080/terminals')
    .then(response => response.json())
    .then(data => {
      this.data = data;
      data._embedded.terminals.forEach(terminal => {
        const option = document.createElement('option');
        option.value = terminal.airportCode;
        option.text = `${terminal.airportCode} - ${terminal.terminalCode}`;
        dropdown.appendChild(option);
      });
    })
    .catch(error => console.error(error));

  dropdown.addEventListener('change', (event) => {
    const selectedOption = event.target.value;
    const selectedTerminals = this.data._embedded.terminals.filter(terminal => terminal.airportCode === selectedOption);
    const selectedAirport = selectedTerminals[0].airportCode;
    window.location.href = `airport.html?code=${selectedAirport}&terminals=${selectedTerminals.length}`;
  });
}

populateDropdown();


//Assuming response from the API has the format of
/*

{
  "airports": [
    {
      "code": "JFK",
      "name": "John F. Kennedy International Airport",
      "terminals": 6
    },
    {
      "code": "LAX",
      "name": "Los Angeles International Airport",
      "terminals": 8
    },
    // ...
  ]
}

*/