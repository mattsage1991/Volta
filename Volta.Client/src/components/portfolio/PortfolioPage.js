import React from 'react';
import {connect} from 'react-redux';
import * as portfolioActions from '../../redux/actions/portfolioActions';
import PropTypes from 'prop-types';
import {bindActionCreators} from "redux";

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
            <form onSubmit={this.handleSubmit}>
                <h2>Portfolio</h2>
                <h3>Add Holding</h3>
                <input type="text" onChange={this.handleChange} value={this.state.holding.tickerSymbol}/>
                <input type="submit" value="Save"/>
                {this.props.portfolio.map(holding => (
                    <div key={holding.tickerSymbol}>{holding.tickerSymbol}</div>
                ))}
            </form>
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