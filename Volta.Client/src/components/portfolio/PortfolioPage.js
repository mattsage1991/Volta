import React from 'react';
import {connect} from 'react-redux';
import * as portfolioActions from '../../redux/actions/portfolioActions';
import PropTypes from 'prop-types';
import {bindActionCreators} from "redux";
import PortfolioTable from "./PortfolioTable";

class PortfolioPage extends React.Component {
    state = {
        holding: {
            tickerSymbol: ""
        }
    };

    handleChange = event => {
        const holding = {...this.state.holding, tickerSymbol: event.target.value};
        this.setState({holding});
    };

    handleSubmit = (event) => {
        event.preventDefault();
        this.props.actions.createHolding(this.state.holding);
    };

    render() {
        return (
            <div className="flex flex-wrap mt-4">
                <div className="w-full mb-12 px-4">
                    <PortfolioTable />
                </div>
            </div>
        );
    }
}

PortfolioPage.propTypes = {
    portfolio: PropTypes.array.isRequired,
    actions: PropTypes.object.isRequired
};

function mapStateToProps(state) {
    return {
        portfolio: state.portfolio
    };
}

function mapDispatchToProps(dispatch) {
    return {
        actions: bindActionCreators(portfolioActions, dispatch)
    };
}

export default connect(mapStateToProps, mapDispatchToProps)(PortfolioPage);