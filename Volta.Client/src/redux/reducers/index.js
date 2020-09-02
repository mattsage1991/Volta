import {combineReducers} from "redux";
import portfolio from "./portfolioReducer";

const rootReducer = combineReducers({
    portfolio
});

export default rootReducer;