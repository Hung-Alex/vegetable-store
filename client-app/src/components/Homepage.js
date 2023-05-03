import React from 'react';
import '../styles/homepage.css';
import Home from './Home';
import Carosel from './Carosel';
import Values from './Values';

const Homepage = (props) => {

    return (
        <div>
            <Carosel buy={props.buy} />
            <Home />
            <Values />
        </div>
    )
}

export default Homepage
