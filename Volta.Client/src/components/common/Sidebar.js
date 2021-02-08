import React from 'react';
import {Link} from "react-router-dom";

const Sidebar = () => {
  
  return (
    <nav className="md:left-0 md:block md:fixed md:top-0 md:bottom-0 md:overflow-y-auto md:flex-row md:flex-no-wrap md:overflow-hidden shadow-xl bg-white flex flex-wrap items-center justify-between relative md:w-64 z-10 py-4 px-6">
        {/* Brand */}
        <Link
            className="md:block text-left md:pb-2 text-gray-700 mr-0 inline-block whitespace-no-wrap text-sm uppercase font-bold p-4 px-0"
            to="/"
          >
            Volta
          </Link>

        {/* Divider */}
        <hr className="my-4 md:min-w-full" />
        
        {/* Navigation */}

        <ul className="md:flex-col md:min-w-full flex flex-col list-none">
            <li className="items-center">
            <Link
                  className={
                    "text-xs uppercase py-3 font-bold block " +
                    (window.location.href.indexOf("/dashboard") !== -1
                      ? "text-blue-500 hover:text-blue-600"
                      : "text-gray-800 hover:text-gray-600")
                  }
                  to="/dashboard"
                >
                  <i
                    className={
                      "fas fa-tv mr-2 text-sm " +
                      (window.location.href.indexOf("/dashboard") !== -1
                        ? "opacity-75"
                        : "text-gray-400")
                    }
                  ></i>{" "}
                  Dashboard
                </Link>
            </li>
            <li className="items-center">
            <Link
                  className={
                    "text-xs uppercase py-3 font-bold block " +
                    (window.location.href.indexOf("/portfolio") !== -1
                      ? "text-blue-500 hover:text-blue-600"
                      : "text-gray-800 hover:text-gray-600")
                  }
                  to="/portfolio"
                >
                  <i
                    className={
                      "fas fa-chart-pie mr-2 text-sm " +
                      (window.location.href.indexOf("/portfolio") !== -1
                        ? "opacity-75"
                        : "text-gray-400")
                    }
                  ></i>{" "}
                  Portfolio
                </Link>
            </li>
        </ul>

        {/* Divider */}
        <hr className="my-4 md:min-w-full" />

        <ul className="md:flex-col md:min-w-full flex flex-col list-none md:mb-4">
          <li className="items-center">
            <Link
              className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
              to="/auth/login"
            >
              <i className="fas fa-fingerprint text-gray-500 mr-2 text-sm"></i>{" "}
              Login
            </Link>
          </li>

          <li className="items-center">
            <Link
              className="text-gray-800 hover:text-gray-600 text-xs uppercase py-3 font-bold block"
              to="/auth/register"
            >
              <i className="fas fa-clipboard-list text-gray-400 mr-2 text-sm"></i>{" "}
              Sign Up
            </Link>
          </li>
        </ul>
    </nav>
  )
};

export default Sidebar;