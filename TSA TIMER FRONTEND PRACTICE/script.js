let data;

function populateDropdown() {
  const dropdown = document.querySelector('#airport-select');

  fetch('data.json')
    .then(response => response.json())
    .then(data => {
      this.data = data;
      data.airports.forEach(airport => {
        const option = document.createElement('option');
        option.value = airport.code;
        option.text = `${airport.code} - ${airport.name}`;
        dropdown.appendChild(option);
      });
    })
    .catch(error => console.error(error));

  dropdown.addEventListener('change', (event) => {
    const selectedOption = event.target.value;
    const selectedAirport = this.data.airports.find(airport => airport.code === selectedOption);
    window.location.href = `airport.html?code=${selectedAirport.code}&name=${selectedAirport.name}&terminals=${selectedAirport.terminals}`;
  });
}


populateDropdown();