import React from 'react';
import '../styles/homepage.css';
import About from './About';
import Carosel from './Carosel';
import Values from './Values';

const Homepage = (props) => {

    return (
        <div>
            <Carosel buy={props.buy} />
            <About />
            <Values />
        </div>
    )
}

export default Homepage
