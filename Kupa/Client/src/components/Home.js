import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render() {
    return (
      <div>
        <h1 className="text-3xl font-bold underline">
            Hello world!
        </h1>
      </div>
    );
  }
}
