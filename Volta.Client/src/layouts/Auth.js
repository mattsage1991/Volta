import React from 'react';
import {Route, Switch, Redirect} from "react-router-dom"
import LoginPage from '../components/login/LoginPage';
import RegisterPage from '../components/register/RegisterPage';

const Auth = () => {
    return (
        <main>
        <section className="relative w-full h-full py-40 min-h-screen">
          <div
            className="absolute top-0 w-full h-full bg-gray-900 bg-no-repeat bg-full"
            style={{
              backgroundImage:
                "url(assets/img/register_bg_2.png)",
            }}
          ></div>
          <Switch>
            <Route path="/auth/login" exact component={LoginPage} />
            <Route path="/auth/register" exact component={RegisterPage} />
            <Redirect from="/auth" to="/auth/login" />
          </Switch>
          <FooterSmall absolute />
        </section>
      </main>
    )
}

export default Auth;