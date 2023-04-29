import React from 'react';
import '../styles/home.css';
import about1 from '../assets/images/about1-min.png';
import about2 from '../assets/images/about2-min.png';
import about3 from '../assets/images/about3-min.png';
import about4 from '../assets/images/hubs-min.png';

function Home() {
    return (
        <div id="about" className="container about">
            <div className="row">
                <div className="col-sm-4">
                    <div className="about-heading"><h2>About Us</h2></div>
                    <div className="about-content"><h4>Agricart is Fresh Produce from the Farm. We are here the solve the problem of farmers who is not getting much profits due to lot of chain between customers and farmers.</h4>
                        <p>We collect Fresh Vegetables and Fruits from Farmers Directly and delivery to customers and small businesses.</p></div>
                    <button className="btn btn-cust about-btn">Get in Touch</button>
                </div>
                <div className="col-sm-8">
                    <ul className="timeline">
                        <li>
                            <div className="direction-r">
                                <div className="flag-wrapper">
                                    <span className="flag"><img src={about1} alt="Farmers" />Farmers</span>
                                </div>
                            </div>
                        </li>

                        <li>
                            <div className="direction-l">
                                <div className="flag-wrapper">
                                    <span className="flag"><img src={about2} alt="Collection Cente" /> Collection Center</span>
                                </div>
                            </div>
                        </li>

                        <li>
                            <div className="direction-r">
                                <div className="flag-wrapper">
                                    <span className="flag"><img src={about3} alt="Distribution Center" />Distribution Center</span>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div className="direction-l">
                                <div className="flag-wrapper">
                                    <span className="flag"><img src={about4} alt="Hubs" />Hubs</span>
                                </div>
                            </div>
                        </li>
                        <li>
                            <div className="direction-r">
                                <div className="flag-wrapper">
                                    <span className="flag"><img src="https://firebasestorage.googleapis.com/v0/b/agricart-c7914.appspot.com/o/banners%2Fcustomers-min.png?alt=media&token=1252f5bf-7aea-4fe9-a379-0d52fba9edb0" alt="Customers" />Customers</span>
                                </div>
                            </div>
                        </li>
                    </ul>
                </div>
            </div>
        </div>
    )
}

export default Home;
