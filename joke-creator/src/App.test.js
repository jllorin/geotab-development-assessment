import React from 'react';
import { render, screen, queryByAttribute, fireEvent, waitFor } from '@testing-library/react';
import App from './App';

const getById = queryByAttribute.bind(null, 'id');

test('renders Joke Generator text', () => {
  render(<App />);
  const linkElement = screen.getByText(/Joke Generator/i);
  expect(linkElement).toBeInTheDocument();
});

describe("The How many jokes do you want", () => {
  test('should call setOptions when text has changed.', () => {
    const setOptions = jest.fn();
    const useStateSpy = jest.spyOn(React, 'useState');
    useStateSpy.mockImplementation((init) => [init, setOptions]);
    const dom = render(<App />);
    const numberText = getById(dom.container, 'no-of-jokes-text');
    expect(numberText).toBeInTheDocument();
    fireEvent.change(numberText, { target: { value: '3' } });
    expect(setOptions).toHaveBeenCalledWith(expect.objectContaining({ noOfJokes: '3'}))
  });
});

describe("The Random Name used", () => {  
  test('should be in the document', async () => {
    const setOptions = jest.fn();
    const useStateSpy = jest.spyOn(React, 'useState');
    useStateSpy.mockImplementation((init) => [init, setOptions]);
    const dom = render(<App />);
    const randomNameUsed = getById(dom.container, 'random-name-used');
    expect(randomNameUsed).toBeInTheDocument();
  });
});