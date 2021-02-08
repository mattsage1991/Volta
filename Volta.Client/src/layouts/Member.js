import React from 'react';
import {Route, Switch} from "react-router-dom"
import HomePage from "../components/home/HomePage";
import Sidebar from "../components/common/Sidebar";
import Navbar from "../components/common/Navbar";
import PageNotFound from "../components/PageNotFound";
import PortfolioPage from "../components/portfolio/PortfolioPage";
import DashboardPage from "../components/dashboard/DashboardPage";

const Member = () => {
    return (
        <>
        <Sidebar />
        <div className="relative md:ml-64 bg-gray-200">
            <Navbar />
            <div className="relative bg-blue-600 md:pt-32 pb-32 pt-12">
                
            </div>
            <div className="px-4 md:px-10 mx-auto w-full -m-24">
                <Switch>
                    <Route exact path="/" component={HomePage}/>
                    <Route path="/dashboard" component={DashboardPage}/>
                    <Route path="/portfolio" component={PortfolioPage}/>
                    <Route component={PageNotFound}/>
                </Switch>
            </div>
        </div>
        </>
    )
}

export default Member;