import http from 'k6/http';
import { sleep } from 'k6';
import { uuidv4 } from 'https://jslib.k6.io/k6-utils/1.4.0/index.js';

export default function () {

    const baseUrl = "https://localhost:44385/api";

    const params = {
        headers: {
            'Content-Type': 'application/json',
        },
    };

    // -----[ register ]-----

    const randomSuffix = uuidv4();
    const email = `pv293_ticketportal_${randomSuffix}@mailinator.com`

    console.log(`user email: ${email}`)

    const registerUserData = {
        "email": email,
        "password": "password"
    }

    let response = http.post(`${baseUrl}/users/registration`, JSON.stringify(registerUserData), params);

    if (response.status !== 200) {
        console.error(`Registration request failed with status code '${response.status}': ${response.status_text}`);
    }

    let responseObject = response.json()
    const userId = responseObject.id;

    console.log('User registered successfully.')

    sleep(1);

    // -----[ sign-in ]-----

    const signInUserData = {
        "email": email,
        "password": "password",
        "rememberMe": true
    }

    response = http.post(`${baseUrl}/users/sign-in`, JSON.stringify(signInUserData), params);

    if (response.status !== 200) {
        console.error(`Sign-in request failed with status code '${response.status}': ${response.status_text}\n${JSON.stringify(response)}\n`);
    }

    responseObject = response.json()

    if (responseObject.id !== userId || responseObject.email !== email) {
        console.error(`Sign-in response mismatch: ${JSON.stringify(responseObject)}`);
    }

    console.log('User signed-in successfully.')

    sleep(1);

    // -----[ check cart exists for registered user ]-----

    response = http.get(`${baseUrl}/carts/current-user`, params);

    if (response.status !== 200) {
        console.error(`Current user cart request failed with status code '${response.status}': ${response.status_text}\n${JSON.stringify(response)}\n`);
    }

    responseObject = response.json()

    if (responseObject.userId !== userId) {
        console.error(`Current user cart response mismatch: ${JSON.stringify(responseObject)}`);
    } else {
        console.log(`User's cart with id '${responseObject.id}' retrieved successfully.`)
    }


    sleep(1);

    // -----[ create event ]-----

    const createEventData = {
        "title": "ABC Concert",
        "description": "Concert of the group ABC",
        "category": 1,
        "start": "2024-03-05",
        "location": "Brno, Street 13/37",
        "url": "www.google.com",
        "hasUnlimitedCapacity": false,
        "totalCapacity": 5
    }

    response = http.post(`${baseUrl}/events`, JSON.stringify(createEventData), params);

    if (response.status !== 201) {
        console.error(`Create event request failed with status code '${response.status}': ${response.status_text}\n${JSON.stringify(response)}\n`);
    }

    const createdEvent = response.json()
    if (createdEvent.title !== createEventData.title) {
        console.error(`Created event data from response does not match request. (${createdEvent.title} vs ${createEventData.title})`)
    } else {
        console.log("Event created successfully.")
    }

    sleep(1);

    // -----[ create event seat category ]-----

    const createSeatCategoryData = {
        "eventId": createdEvent.id,
        "name": "Tier1",
        "price": 119
    }

    response = http.post(`${baseUrl}/events/${createdEvent.id}/seat-category`, JSON.stringify(createSeatCategoryData), params);

    // TODO: ideally should be 201 after GET endpoint is created
    if (response.status !== 200) {
        console.error(`Create seat category request failed with status code '${response.status}': ${response.status_text}\n${JSON.stringify(response)}\n`);
    }


    var createdSeatCategory = response.json();
    if (createdSeatCategory.name !== createSeatCategoryData.name) {
        console.error(`Created seat category data from response does not match request. (${createdSeatCategory.name} vs ${createSeatCategoryData.name})`)
    } else {
        console.log("Seat category for event created successfully.")
    }


    sleep(1);

    // -----[ create event seat category ]-----

    const createSeatsData = [
        {
            "sectorCode": "C",
            "rowNumber": 1,
            "seatNumber": 1,
            "categoryId": createdSeatCategory.id,
            "eventId": createdEvent.id,
            "state": 1
        },
        {
            "sectorCode": "C",
            "rowNumber": 1,
            "seatNumber": 2,
            "categoryId": createdSeatCategory.id,
            "eventId": createdEvent.id,
            "state": 1
        },
        {
            "sectorCode": "C",
            "rowNumber": 1,
            "seatNumber": 3,
            "categoryId": createdSeatCategory.id,
            "eventId": createdEvent.id,
            "state": 1
        },
        {
            "sectorCode": "B3",
            "rowNumber": 2,
            "seatNumber": 3,
            "categoryId": createdSeatCategory.id,
            "eventId": createdEvent.id,
            "state": 1
        }
    ]

    response = http.post(`${baseUrl}/events/${createdEvent.id}/seat-layout`, JSON.stringify(createSeatsData), params);

    // TODO: ideally should be 201 after GET endpoint is created
    if (response.status !== 200) {
        console.error(`Create seat layout request failed with status code '${response.status}': ${response.status_text}\n${JSON.stringify(response)}\n`);
    }

    var createdSeats = response.json();

    if (!createdSeats.map(x => x.sectorCode).includes("B3")) {
        console.error(`Created seat category data from response does not match request.`)
    } else {
        console.log("Seat layout for event created successfully.")
    }

    var createdSeat = createdSeats[0]

    sleep(1);

    // -----[ create event seat category ]-----

    const addTicketToCartData = {
        "eventId": createdEvent.id,
        "seatId": createdSeat.id
    }

    response = http.post(`${baseUrl}/tickets/`, JSON.stringify(addTicketToCartData), params);

    // TODO: ideally should be 201 after GET endpoint is created
    if (response.status !== 200) {
        console.error(`Add ticket to cart request failed with status code '${response.status}': ${response.status_text}\n${JSON.stringify(response)}\n`);
    }

    var createdTicket = response.json();

    if (createdTicket.eventId !== addTicketToCartData.eventId) {
        console.error(`Created ticket data from response does not match request.`)
    } else {
        console.log("Ticket added to cart successfully.")
    }

    sleep(1);

}
