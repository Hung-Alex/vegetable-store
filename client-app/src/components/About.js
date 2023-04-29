import React from 'react';
import '../styles/about.css';
import banner from '../assets/images/bg_1.jpg';
import img1 from '../assets/images/about.jpg';

class About extends React.Component {
    render() {
        return (
            <>
                <div className='about-banner-container'>
                    <div className='about-banner-content'>
                        <span>HOME ABOUT US</span>
                        <h1>ABOUT US</h1>
                    </div>
                </div>
                <div className='about-container'>
                    <div className='img'></div>
                    <div className='about-container-content'>
                        <h1>Welcome to Vegefoods an eCommerce website</h1>
                        <p>
                            Far far away, behind the word mountains, far from the countries Vokalia and Consonantia,
                            there live the blind texts. Separated they live in Bookmarksgrove right at the coast of the Semantics,
                            a large language ocean.
                        </p>
                        <p>
                            But nothing the copy said could convince her and so it didn’t take long until a few insidious Copy Writers ambushed her,
                            made her drunk with Longe and Parole and dragged her into their agency, where they abused her for their.
                        </p>
                        <p>
                            <a className='button'>Shop now</a>
                        </p>
                    </div>
                </div>
                <div className='about-subcribe'>
                    <div className='about-subcribe-container'>
                        <h3>Subcribe to our Newsletter</h3>
                        <p>Get e-mail updates about our latest shops and special offers</p>
                    </div>
                    <div className='about-subcribe-input'>
                        <input type='text' placeholder='Enter email address' />
                        <button className='subcribe'>Subcribe</button>
                    </div>
                </div>
                <div className='about-banner1-container'>
                    <div className='about-banner1-content'>
                        <span>
                            <h1>10,000</h1>
                            <p>HAPPY CUSTOMERS</p>
                        </span>
                        <span>
                            <h1>100</h1>
                            <p>BRANCHES</p>
                        </span>
                        <span>
                            <h1>1,000</h1>
                            <p>PARTNER</p>
                        </span>
                        <span>
                            <h1>100</h1>
                            <p>AWARDS</p>
                        </span>
                    </div>
                </div>

            </>
        )
    }
}

export default About;