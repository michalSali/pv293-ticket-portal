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

    sleep(3);

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

    sleep(1);

    // -----[ check cart exists for registered user ]-----

    response = http.get(`${baseUrl}/carts/current-user`, params);

    if (response.status !== 200) {
        console.error(`Current user cart request failed with status code '${response.status}': ${response.status_text}\n${JSON.stringify(response)}\n`);
    }

    responseObject = response.json()

    if (responseObject.userId !== userId) {
        console.error(`Current user cart response mismatch: ${JSON.stringify(responseObject)}`);
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

    var createdEvent = response.body;
    if (createdEvent.title !== createEventData.title) {
        console.error(`Created event data from response does not match request. (${createdEvent.title} vs ${createEventData.title})`)
    }

    sleep(1);

    // -----[ create event seat category ]-----

    //const createEventData = {
    //    "eventId": createdEvent.id,
    //    "name": "Tier1",
    //    "price": 119
    //},

    //response = http.post(`${baseUrl}/events`, JSON.stringify(createEventData), params);

    //// TODO: ideally should be 201 after GET endpoint is created
    //if (response.status !== 200) {
    //    console.error(`Create seat category request failed with status code '${response.status}': ${response.status_text}\n${response}\n`);
    //}

    //var createdEvent = response.body;
    //if (createdEvent.title !== createEventData.title) {
    //    console.error(`Created event data from response does not match request. (${createdEvent.title} vs ${createEventData.title})`)
    //}

    //sleep(1);

}
