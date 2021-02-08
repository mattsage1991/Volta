import React from "react";
import {Route, Switch} from "react-router-dom"
import HomePage from "./home/HomePage";
import AboutPage from "./about/AboutPage";
import Member from "../layouts/Member"
import PageNotFound from "./PageNotFound";
import '@fortawesome/fontawesome-free/js/all'
import "../assets/styles/tailwind.css";
import Auth from "../layouts/Auth";

function App() {
    return (
        <>            
            <Switch>
                <Route exact path="/" component={HomePage}/>
                <Route path="/member" component={Member}/>
                <Route path="/auth" component={Auth}/>
                <Route path="/about" component={AboutPage}/>
                <Route component={PageNotFound}/>
            </Switch>               
        </>
    );
}

export default App;