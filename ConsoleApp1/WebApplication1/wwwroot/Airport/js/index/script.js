const btnIncomingFlights = document.getElementById('btnIncomingFlights')
const btnDepartingFlights = document.getElementById('btnDepartingFlights')
const flightStatuses = ['Departed', 'On Time', 'Expected', 'Landed', 'Canceled', 'Delayed']
btnIncomingFlights.addEventListener('click', async (e) => {
    e.target.classList.remove('btn-outline-light')
    e.target.classList.add('btn-light')
    btnDepartingFlights.classList.remove('btn-light')
    btnDepartingFlights.classList.add('btn-outline-light')
    const port = getCurrentPort()
    const incomingFlightsURL = `https://localhost:${port}/api/IncomingFlightsInfoApi`
    let incomingFlights = []
    await fetch(incomingFlightsURL).then(response => response.json()).then(data =>
    {
        incomingFlights = data
    })
    const ifObj = {
        incomingFlights,
        areIncoming: true
    }
    updateTable(ifObj)
})

btnDepartingFlights.addEventListener('click', async (e) => {
    e.target.classList.remove('btn-outline-light')
    e.target.classList.add('btn-light')
    btnIncomingFlights.classList.remove('btn-light')
    btnIncomingFlights.classList.add('btn-outline-light')
    const port = getCurrentPort()
    const departingFlightsURL = `https://localhost:${port}/api/DepartingFlightsInfoApi`
    let departingFlights = []
    await fetch(departingFlightsURL).then(response => response.json()).then(data =>
    {
        departingFlights = data
    })
    const dfObj = {
        departingFlights,
        areDeparting: true
    }
    updateTable(dfObj)
})

function updateTable(dataObj) {
    let table = document.getElementById('flights-table').tBodies[0]
    let headerPlace = document.getElementById('place')
    let headerDate = document.getElementById('date')
    let rowsCount = table.rows.length
    for (let i = 0; i < rowsCount; i++) {
        table.deleteRow(0)
    }
    if (dataObj.areDeparting) {
        headerPlace.innerText = 'Куда'
        headerDate.innerText = 'Время вылета'
        for (let i = 0; i < dataObj.departingFlights.length; i++) {
            let obj = dataObj.departingFlights[i]
            let row = table.insertRow(-1)
            let cellIndex = 0
            for (let prop in obj) {
                let cell = row.insertCell(cellIndex)
                if (prop === 'tailNumber') {
                    cell.outerHTML = `<th>${obj[prop]}</th>`
                    cellIndex++
                    continue
                }
                if (prop === 'flightStatus') {
                    let status = flightStatuses[obj[prop]]
                    if (status === 'Canceled') cell.classList.add('table-warning')
                    else if (status === 'Departed') cell.classList.add('table-success')
                    else if (status === 'Delayed') cell.classList.add('table-warning')
                    cell.innerText = status
                    cellIndex++
                    continue
                }
                cell.innerText = obj[prop]
                cellIndex++
            }
        }
    } else {
        headerPlace.innerText = 'Откуда'
        headerDate.innerText = 'Время прибытия'
        for (let i = 0; i < dataObj.incomingFlights.length; i++) {
            let obj = dataObj.incomingFlights[i]
            let row = table.insertRow(-1)
            let cellIndex = 0
            for (let prop in obj) {
                let cell = row.insertCell(cellIndex)
                if (prop === 'tailNumber')
                {
                    cell.outerHTML = `<th>${obj[prop]}</th>`
                    cellIndex++
                    continue
                }
                if (prop === 'flightStatus') {
                    let status = flightStatuses[obj[prop]]
                    if (status === 'Delayed') cell.classList.add('table-danger')
                    else if (status === 'Landed') cell.classList.add('table-success')
                    cell.innerText = status
                    cellIndex++
                    continue
                }
                cell.innerText = obj[prop]
                cellIndex++
            }
        }
    }

}

function getCurrentPort() {
    return window.location.href.split('/')[2].split(':')[1]
}