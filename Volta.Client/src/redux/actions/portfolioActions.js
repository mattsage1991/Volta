import * as types from "./actionTypes"

export function createHolding(holding) {
    return {type: types.CREATE_HOLDING, holding};
}