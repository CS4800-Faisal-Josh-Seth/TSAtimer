let data;

function populateDropdown() {
  const dropdown = document.querySelector('#airport-select');

  fetch('airports.json')
    .then(response => response.json())
    .then(jsonData => {
      data = jsonData;
      data.airports.forEach(airport => {
        const option = document.createElement('option');
        option.value = airport.code;
        option.text = `${airport.name}`;
        dropdown.appendChild(option);
      });
    })
    .catch(error => console.error(error));

  dropdown.addEventListener('change', (event) => {
    const selectedOption = event.target.value;
    const selectedAirport = data.airports.find(airport => airport.code === selectedOption);
    window.location.href = `airport.html?code=${selectedAirport.code}&terminals=${selectedAirport.terminals}`;
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