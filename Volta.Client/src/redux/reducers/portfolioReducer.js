import * as types from "../actions/actionTypes";

export default function portfolioReducer(state = [], action) {
    switch (action.type) {
        case types.CREATE_HOLDING:
            return [...state, {...action.holding}];
        default:
            return state;
    }
}